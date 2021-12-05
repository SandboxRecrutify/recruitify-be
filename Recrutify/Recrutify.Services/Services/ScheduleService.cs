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

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IValidator<IEnumerable<ScheduleSlot>> validator, IMapper mapper, IUserProvider userProvider, IScheduleSlotHelper scheduleSlotHelper, ICandidateService candidateService)
        {
            _projectService = projectService;
            _candidateService = candidateService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _userProvider = userProvider;
            _scheduleSlotHelper = scheduleSlotHelper;
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

            var cancelledInterviews = _mapper.Map<IEnumerable<Interview>>(interviews.Where(i => i.IsAppointment == false));
            var candidatesIdsOfCancelledInterviews = cancelledInterviews.Select(i => i.UserId);

            if (cancelledInterviews.Any())
            {
                await _scheduleRepository.BulkСancelInterviewsAsync(cancelledInterviews);
                await _candidateService
            }

            var appointedInterviews = _mapper.Map<IEnumerable<Interview>>(interviews.Where(i => i.IsAppointment == true));
            var candidatesIdsOfAppointedInterviews = cancelledInterviews.Select(i => i.UserId);

            if (appointedInterviews.Any())
            {
                await _scheduleRepository.BulkAppointInterviewsAsync(appointedInterviews);
            }
        }

        private IEnumerable<DateTime> GetDateTimeInScheduleSlots(IEnumerable<ScheduleSlot> scheduleSlots)
        {
            return scheduleSlots?.Select(s => s.AvailableTime) ?? new List<DateTime>();
        }
    }
}
