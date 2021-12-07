using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
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
        private readonly IValidator<IEnumerable<ScheduleSlot>> _validatorForScheduleSlot;
        private readonly IValidator<IEnumerable<InterviewDTO>> _validatorInterview;
        private readonly IStatusHelper _statusHelper;
        private readonly IUserService _userService;
        private readonly IInviteEventPublisher _inviteEventPublisher;

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IUserService userService, IInviteEventPublisher inviteEventPublisher, IValidator<IEnumerable<ScheduleSlot>> validatorForScheduleSlot, IValidator<IEnumerable<InterviewDTO>> validatorInterview, IMapper mapper, IUserProvider userProvider, IScheduleSlotHelper scheduleSlotHelper, ICandidateService candidateService, IStatusHelper statusHelper)
        {
            _projectService = projectService;
            _candidateService = candidateService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _userProvider = userProvider;
            _scheduleSlotHelper = scheduleSlotHelper;
            _validatorForScheduleSlot = validatorForScheduleSlot;
            _statusHelper = statusHelper;
            _userService = userService;
            _inviteEventPublisher = inviteEventPublisher;
            _validatorInterview = validatorInterview;
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

        public async Task UpdateScheduleSlotsForCurrentUserAsync(IEnumerable<DateTime> dates, DateTime? weekStart)
        {
            DateTime periodStartDate = default;
            var currentUserId = _userProvider.GetUserId();
            if (dates.Count() != 0)
            {
                var minDateTime = dates.Min();
                var dayNum = (int)minDateTime.DayOfWeek;
                if (dayNum == 0)
                {
                    dayNum = Constants.Week.CountDays;
                }

                periodStartDate = minDateTime.Date.AddDays(1 - dayNum);
            }
            else if (dates.Count() == 0 && weekStart.HasValue)
            {
                periodStartDate = weekStart.Value;
            }
            else
            {
                throw new ValidationException("Unknown start of the week.");
            }

            var periodFinishDate = periodStartDate.AddDays(Constants.Week.CountDays - 1);

            dates = dates.Where(dt => dt < periodFinishDate).ToList();

            var scheduleSlotsOfCurrentUser = await _scheduleRepository.GetScheduleSlotsOfDatePeriodAsync(currentUserId, periodStartDate, periodFinishDate);

            var removedListDateTime = _scheduleSlotHelper.GetRemovedDateTimeInSheduleSlots(GetDateTimeInScheduleSlots(scheduleSlotsOfCurrentUser), dates);
            if (removedListDateTime.Any())
            {
                var validationResult = await _validatorForScheduleSlot.ValidateAsync(scheduleSlotsOfCurrentUser.Where(s => removedListDateTime.Contains(s.AvailableTime)));
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
                throw new ValidationException("Project does not exist.");
            }

            var validationResult = await _validatorInterview.ValidateAsync(interviews);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var allCandidates = await _candidateService.GetCandidatesByIdsAsync(interviews.Select(i => i.CandidateId));
            var dictionaryInterviews = _mapper.Map<IEnumerable<Interview>>(interviews)
                                                .GroupBy(i => i.IsAppointment)
                                                .ToDictionary(k => k.Key, v => v.ToList());

            var canceledInterviews = dictionaryInterviews.TryGetValue(false, out var listCanceledInterviews) ? listCanceledInterviews : default;
            var appointedInterviews = dictionaryInterviews.TryGetValue(true, out var listAppointedInterviews) ? listAppointedInterviews : default;

            if (canceledInterviews != null)
            {
                await BulkСancelInterviewsAsync(projectId, allCandidates, canceledInterviews);
            }

            if (appointedInterviews != null)
            {
                await BulkAppointInterviewsAsync(projectId, allCandidates, appointedInterviews);
            }
        }

        private async Task BulkСancelInterviewsAsync(Guid projectId, IEnumerable<Candidate> candidates, IEnumerable<Interview> interviews)
        {
            var canceledСandidateIds = interviews.Select(i => i.CandidateId).ToList();
            var currentStatusCandidates = candidates.FirstOrDefault(c => canceledСandidateIds.Contains(c.Id)).ProjectResults.FirstOrDefault(p => p.ProjectId == projectId).Status;

            await _scheduleRepository.BulkСancelInterviewsAsync(interviews);
            await _candidateService.BulkUpdateStatusAsync(canceledСandidateIds, projectId, _statusHelper.GetStatusDown(currentStatusCandidates));
        }

        private async Task BulkAppointInterviewsAsync(Guid projectId, IEnumerable<Candidate> candidates, IEnumerable<Interview> interviews)
        {
            var appointedСandidateIds = interviews.Select(i => i.CandidateId).ToList();
            var appointedСandidate = candidates.Where(c => appointedСandidateIds.Contains(c.Id)).ToList();
            var currentStatusCandidates = _statusHelper.GetStatusUp(appointedСandidate.FirstOrDefault().ProjectResults.FirstOrDefault(p => p.ProjectId == projectId).Status);
            var usersForInvite = await _userService.GetUsersShortByIdsAsync(interviews.Select(i => i.UserId));
            var candidatesForInvite = _mapper.Map<IEnumerable<CandidateShort>>(appointedСandidate);

            var args = new AssignedInterviewEventArgs()
            {
                Interviews = interviews
                                .Select(i => new InterviewEmailInfo()
                                {
                                    AppoitmentDateTime = i.AppoitmentDateTime,
                                    InterviewType = (InterviewType)Enum.Parse(typeof(InterviewType), currentStatusCandidates.ToString(), true),
                                    Candidate = candidatesForInvite.FirstOrDefault(c => c.Id == i.CandidateId),
                                    User = usersForInvite.FirstOrDefault(u => u.Id == i.UserId),
                                }),
            };
            await _scheduleRepository.BulkAppointInterviewsAsync(
                    interviews,
                    candidates.Select(c =>
                    {
                        var candidateInfo = _mapper.Map<ScheduleCandidateInfo>(c);
                        var projectResult = c.ProjectResults.FirstOrDefault(p => p.ProjectId == projectId);
                        projectResult.Status = _statusHelper.GetStatusUp(projectResult.Status);
                        candidateInfo.ProjectResult = _mapper.Map<ScheduleCandidateProjectResult>(projectResult);
                        return candidateInfo;
                    }));
            await _candidateService.BulkUpdateStatusAsync(appointedСandidateIds, projectId, currentStatusCandidates);
            _inviteEventPublisher.OnAssignedInterview(args);
        }

        private IEnumerable<DateTime> GetDateTimeInScheduleSlots(IEnumerable<ScheduleSlot> scheduleSlots)
        {
            return scheduleSlots?.Select(s => s.AvailableTime) ?? new List<DateTime>();
        }
    }
}
