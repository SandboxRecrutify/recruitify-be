using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.Constant;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Helpers.Abstract;
using Recrutify.Services.Providers;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IProjectService _projectService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ICandidateService _candidateService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;
        private readonly IScheduleSlotHelper _scheduleSlotHelper;
        private readonly IInviteEventPublisher _inviteEventPublisher;
        private readonly IValidator<IEnumerable<ScheduleSlot>> _validator;

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IValidator<IEnumerable<ScheduleSlot>> validator, IMapper mapper, IUserProvider userProvider, IScheduleSlotHelper scheduleSlotHelper, ICandidateService candidateService, IInviteEventPublisher inviteEventPublisher, IUserService userService)
        {
            _projectService = projectService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _candidateService = candidateService;
            _userProvider = userProvider;
            _userService = userService;
            _scheduleSlotHelper = scheduleSlotHelper;
            _inviteEventPublisher = inviteEventPublisher;
            _validator = validator;
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

        public async Task BulkAppointOrCancelInterviewsAsync(IEnumerable<InterviewAppointmentDTO> interviewAppointmentDTOs, Guid projectId)
        {
            var candidates = await _candidateService.GetCandidatesByIdsAsync(interviewAppointmentDTOs.Select(c => c.CandidateId));
            var scheduleCandidateInfos = GetScheduleCandidateInfos(interviewAppointmentDTOs, candidates, projectId);
            var interviewAppointments = interviewAppointmentDTOs.Select(a => new InterviewAppointment()
            {
                ScheduleSlot = new ScheduleSlot()
                {
                    AvailableTime = a.DateTime,
                    ScheduleCandidateInfo = _mapper.Map<ScheduleCandidateInfo>(scheduleCandidateInfos.FirstOrDefault(c => c.Id == a.CandidateId)),
                },
                UserId = a.UserId,
                IsAppointment = a.IsAppointment,
            }).ToList();

            await _scheduleRepository.UpdateScheduleSlotsCandidateInfoAsync(interviewAppointments);
            await _candidateService.UpdateIsAssignedAndStatusAsync(interviewAppointmentDTOs, candidates, projectId);

            if (interviewAppointmentDTOs.Any(i => i.IsAppointment == true))
            {
                var interview = await GetInterviews(candidates, interviewAppointmentDTOs, projectId);
                _inviteEventPublisher.OnAssignedInterview(new Events.AssignedInterviewEventArgs() { Interviews = interview });
            }
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

        private async Task<IEnumerable<Interview>> GetInterviews(IEnumerable<CandidateDTO> candidates, IEnumerable<InterviewAppointmentDTO> interviewAppointmentDTOs, Guid projectId)
        {
            var nameAndEmailUsers = await _userService.GetNamesAndEmailsByIdsAsync(interviewAppointmentDTOs.Where(i => i.IsAppointment == true).Select(i => i.UserId));
            var interviews = new List<Interview>();
            foreach (var candidate in candidates)
            {
                var userId = interviewAppointmentDTOs.FirstOrDefault(i => i.CandidateId == candidate.Id && i.IsAppointment)?.UserId;
                if (userId != null)
                {
                    var projectResult = candidate.ProjectResults.FirstOrDefault(pr => pr.ProjectId == projectId);
                    var interview = _mapper.Map<Interview>(candidate);
                    interview.Candidate.Skype = candidate.Contacts.FirstOrDefault(c => c.Type == DataAccess.Constants.Contacts.Skype)?.Value;
                    interview.User = new UserEmailInfo()
                    {
                        Id = userId.Value,
                        Email = nameAndEmailUsers.TryGetValue(userId.Value, out var strEmail) ? strEmail.Split(',')[0] : default,
                        Name = nameAndEmailUsers.TryGetValue(userId.Value, out var strName) ? strName.Split(',')[1] : default,
                    };
                    interview.AppointDateTimeUtc = interviewAppointmentDTOs.FirstOrDefault(i => i.CandidateId == candidate.Id).DateTime;
                    interview.InterviewType = projectResult.Status == StatusDTO.Test ? InterviewType.RecruityInterview
                        : projectResult.Status == StatusDTO.RecruiterInterview ? InterviewType.TechnicalInterviewOne
                        : InterviewType.TechnicalInterviewSecond;
                    interviews.Add(interview);
                }
            }

            return interviews;
        }

        private IEnumerable<DateTime> GetDateTimeInScheduleSlots(IEnumerable<ScheduleSlot> scheduleSlots)
        {
            return scheduleSlots?.Select(s => s.AvailableTime) ?? new List<DateTime>();
        }

        private IEnumerable<ScheduleCandidateInfoDTO> GetScheduleCandidateInfos(IEnumerable<InterviewAppointmentDTO> interviewAppointmentDTOs, IEnumerable<CandidateDTO> candidates, Guid projectId)
        {
            var scheduleCandidateInfos = new List<ScheduleCandidateInfoDTO>();
            foreach (var interviewAppontment in interviewAppointmentDTOs)
            {
                var candidate = candidates.FirstOrDefault(c => c.Id == interviewAppontment.CandidateId);
                var projectResult = candidate.ProjectResults.FirstOrDefault(pr => pr.ProjectId == projectId);
                var scheduleCandidateInfo = new ScheduleCandidateInfoDTO();
                if (interviewAppontment.IsAppointment)
                {
                    scheduleCandidateInfo = _mapper.Map<ScheduleCandidateInfoDTO>(candidate);
                    scheduleCandidateInfo.ProjectResult = new ScheduleCandidateProjectResultDTO()
                    {
                        IsAssignedOnInterview = true,
                        PrimarySkill = projectResult.PrimarySkill,
                        ProjectId = projectResult.ProjectId,
                        Status = projectResult.Status + 1,
                    };
                }

                scheduleCandidateInfos.Add(scheduleCandidateInfo);
            }

            return scheduleCandidateInfos;
        }
    }
}
