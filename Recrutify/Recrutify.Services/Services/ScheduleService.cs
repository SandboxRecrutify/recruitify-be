using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Helpers.Abstract;
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

        public async Task<IEnumerable<ScheduleDTO>> GetByDatePeriodForCurrentUserAsync(DateTime? date, int daysNum)
        {
            if (!date.HasValue)
            {
                date = DateTime.UtcNow.Date;
            }

            var userId = _userProvider.GetUserId();
            var schedules = await _scheduleRepository.GetByDatePeriodAsync(userId, date.Value, daysNum);
            return _mapper.Map<List<ScheduleDTO>>(schedules);
        }
    }
}
