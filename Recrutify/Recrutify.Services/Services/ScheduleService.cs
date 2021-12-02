using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;

using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Providers;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IProjectService _projectService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IMapper mapper, IUserProvider userProvider)
        {
            _projectService = projectService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _userProvider = userProvider;
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

        public async Task UpdateSlotsForCurrentUser(IEnumerable<DateTime> timeSlots, DateTime mondayDate)
        {
            var userId = _userProvider.GetUserId();
            var timeSlotsForUser = new TimeSlotsForUserDTO()
            {
                UserId = userId,
                TimeSlots = timeSlots,
                MondayDate = mondayDate,
            };
            var currentSchedule = await _scheduleRepository.GetByUserIdAsync(userId);
            var updatedSchedule = UpdateTimeSlotsForTimePeriod(currentSchedule, timeSlots, mondayDate);
            await _scheduleRepository.UpdateAsync(updatedSchedule);
        }

        private Schedule UpdateTimeSlotsForTimePeriod(Schedule currentSchedule, IEnumerable<DateTime> newTimeSlots, DateTime mondayDate)
        {
            var updatedScheduleForUser = currentSchedule;
            var newScheduleSlots = newTimeSlots
                .Select(s =>
                    new ScheduleSlot
                    {
                        AvailableTime = s,
                        ScheduleCandidateInfo = null,
                    }).ToList();
            var assaignedScheduleSlots = currentSchedule.ScheduleSlots
                           .Select(s => s)
                           .Where(s => s.ScheduleCandidateInfo != null);
            var currentScheduleSlotsForTimePeriod = currentSchedule.ScheduleSlots
                           .Select(s => s)
                           .Where(s => s != null
                           && s.AvailableTime >= mondayDate
                           && s.AvailableTime <= mondayDate.AddDays(Constants.Week.NumberOfDays));
            var newSlotsWithoutAssigned = newScheduleSlots;
            newSlotsWithoutAssigned
                .RemoveAll(s => assaignedScheduleSlots
                                .Any(a => a.AvailableTime == s.AvailableTime));
            updatedScheduleForUser.ScheduleSlots = updatedScheduleForUser.ScheduleSlots
                .Union(newSlotsWithoutAssigned)
                .Except(currentScheduleSlotsForTimePeriod
                        .Except(newScheduleSlots)
                        .Except(assaignedScheduleSlots));
            return updatedScheduleForUser;
        }
    }
}
