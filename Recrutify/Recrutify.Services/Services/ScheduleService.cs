using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.Constant;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Events;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Helpers.Abstract;
using Recrutify.Services.Providers;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IProjectService _projectService;
        private readonly ICandidateService _candidateService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;
        private readonly IScheduleSlotHelper _scheduleSlotHelper;
        private readonly IValidator<IEnumerable<ScheduleSlot>> _validator;
        private readonly IStatusHelper _statusHelper;
        private readonly IUserService _userService;
        private readonly IInviteEventPublisher _inviteEventPublisher;

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IUserService userService, IInviteEventPublisher inviteEventPublisher, IValidator<IEnumerable<ScheduleSlot>> validator, IMapper mapper, IUserProvider userProvider, IScheduleSlotHelper scheduleSlotHelper, ICandidateService candidateService, IStatusHelper statusHelper)
        {
            _projectService = projectService;
            _candidateService = candidateService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _userProvider = userProvider;
            _scheduleSlotHelper = scheduleSlotHelper;
            _validator = validator;
            _statusHelper = statusHelper;
            _userService = userService;
            _inviteEventPublisher = inviteEventPublisher;
        }

        public async Task<IEnumerable<ScheduleDTO>> GetByUserPrimarySkillAsync(Guid projectId, DateTime? date, Guid primarySkillId)
        {
            if (!date.HasValue)
            {
                date = DateTime.UtcNow.Date;
            }

            var usersIds = await _projectService.GetInterviewersIdsAsync(projectId);
            var schedules = await _scheduleRepository.GetByUserPrimarySkillAsync(usersIds, date.Value, primarySkillId);
            return _mapper.Map<List<ScheduleDTO>>(schedules);
        }

        public async Task<ScheduleDTO> GetByDatePeriodForCurrentUserAsync(DateTime? date, int daysNum)
        {
            if (!date.HasValue)
            {
                date = DateTime.UtcNow.Date;
            }

            var userId = _userProvider.GetUserId();
            var schedules = await _scheduleRepository.GetByDatePeriodAsync(userId, date.Value, daysNum);
            return _mapper.Map<ScheduleDTO>(schedules);
        }

        public async Task UpdateScheduleSlotsForCurrentUserAsync(IEnumerable<DateTime> dates)
        {
            var currentUserId = _userProvider.GetUserId();
            var periodStartDate = dates.Min();
            var periodFinishDate = periodStartDate.Date.AddDays(Constants.Week.CountDays - (int)periodStartDate.DayOfWeek + 1);
            dates = dates.Where(dt => dt < periodFinishDate).ToList();

            var scheduleSlotsOfCurrentUser = await _scheduleRepository.GetScheduleSlotsOfDatePeriodAsync(currentUserId, periodStartDate, periodFinishDate);

            var removedListDateTime = _scheduleSlotHelper.GetRemovedDateTimeInSheduleSlots(GetDateTimeInScheduleSlots(scheduleSlotsOfCurrentUser), dates);
            if (removedListDateTime.Any())
            {
                var validationResult = await _validator.ValidateAsync(scheduleSlotsOfCurrentUser.Where(s => removedListDateTime.Contains(s.AvailableTime)));
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }

            var newListDateTime = _scheduleSlotHelper.GetAddedDateTimeInSheduleSlots(GetDateTimeInScheduleSlots(scheduleSlotsOfCurrentUser), dates);

            await _scheduleRepository.BulkUpdateScheduleSlotsAsync(currentUserId, newListDateTime, removedListDateTime);
        }

        public async Task ProcessingAppointOrCancelInterviewsAsync(IEnumerable<InterviewDTO> interviews, Guid projectId, CancellationToken cancellationToken)
        {
            var projectExists = await _projectService.ExistsAsync(projectId, cancellationToken);
            if (!projectExists)
            {
                throw new Exception("Project does not exist.");
            }

            var allCadidates = await _candidateService.GetCandidatesByIdsAsync(interviews.Select(i => i.CandidateId));

            var cancelledInterviews = _mapper.Map<IEnumerable<Interview>>(interviews.Where(i => i.IsAppointment == false));
            var cadidatesIdsCancelledInterviews = cancelledInterviews.Select(i => i.CandidateId);
            var cuurentStatusCadidates = allCadidates.Where(c => cadidatesIdsCancelledInterviews.Contains(c.Id)).Select(c => c.ProjectResults.FirstOrDefault(p => p.ProjectId == projectId).Status).FirstOrDefault();

            if (cancelledInterviews.Any())
            {
                await _scheduleRepository.BulkСancelInterviewsAsync(cancelledInterviews);
                await _candidateService.BulkUpdateStatusAsync(cancelledInterviews.Select(i => i.CandidateId), projectId, _statusHelper.GetStatusDown(cuurentStatusCadidates));
            }

            var appointedInterviews = _mapper.Map<IEnumerable<Interview>>(interviews.Where(i => i.IsAppointment == true));
            var users = await _userService.GetUsersShortByIdsAsync(appointedInterviews.Select(i => i.UserId));
            var cadidatesIdsAppointedInterviews = appointedInterviews.Select(i => i.CandidateId);
            cuurentStatusCadidates = allCadidates.Where(c => cadidatesIdsAppointedInterviews.Contains(c.Id)).Select(c => c.ProjectResults.FirstOrDefault(p => p.ProjectId == projectId).Status).FirstOrDefault();

            var usersForInvite = users.Select(c => new UserEmailInfo() { Id = c.Id, Email = c.Email, Name = c.Name }).ToList();
            var candidatesForInvite = allCadidates.Where(c => cadidatesIdsAppointedInterviews.Contains(c.Id))
                                        .Select(c => new CandidateEmailInfo()
                                        {
                                            Email = c.Email,
                                            Id = c.Id,
                                            Name = c.Name,
                                            PhoneNumber = c.PhoneNumber,
                                            Skype = c?.Contacts?.FirstOrDefault(s => s.Type == "Skype")?.Value,
                                        }).ToList();

            var args = new AssignedInterviewEventArgs()
            {
                Interviews = appointedInterviews
                                .Select(i => new InterviewEmailInfo()
                                {
                                    AppointDateTimeUtc = i.AppointDateTimeUtc,
                                    InterviewType = (InterviewType)Enum.Parse(typeof(InterviewType), cuurentStatusCadidates.ToString(), true),
                                    Candidate = candidatesForInvite.FirstOrDefault(c => c.Id == i.CandidateId),
                                    User = usersForInvite.FirstOrDefault(u => u.Id == i.UserId),
                                }),
            };

            if (appointedInterviews.Any())
            {
                await _scheduleRepository.BulkAppointInterviewsAsync(
                    appointedInterviews,
                    allCadidates.Where(c => cadidatesIdsAppointedInterviews.Contains(c.Id)).Select(c => new ScheduleCandidateInfo()
                    {
                         BestTimeToConnect = c.BestTimeToConnect,
                         Id = c.Id,
                         Name = c.Name,
                         Surname = c.Surname,
                         Email = c.Email,
                         Skype = c?.Contacts?.FirstOrDefault(s => s.Type == "Skype")?.Value,
                         ProjectResult = c.ProjectResults
                                              .Select(p => new ScheduleCandidateProjectResult()
                                              {
                                                  ProjectId = p.ProjectId,
                                                  IsAssignedOnInterview = p.IsAssignedOnInterview,
                                                  PrimarySkill = p.PrimarySkill,
                                                  Status = _statusHelper.GetStatusUp(p.Status),
                                              })
                                              .FirstOrDefault(p => p.ProjectId == projectId),
                    }));
                await _candidateService.BulkUpdateStatusAsync(appointedInterviews.Select(i => i.CandidateId), projectId, _statusHelper.GetStatusUp(cuurentStatusCadidates));
                _inviteEventPublisher.OnAssignedInterview(args);
            }
        }

        private IEnumerable<DateTime> GetDateTimeInScheduleSlots(IEnumerable<ScheduleSlot> scheduleSlots)
        {
            return scheduleSlots?.Select(s => s.AvailableTime) ?? new List<DateTime>();
        }
    }
}
