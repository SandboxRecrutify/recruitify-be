using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IMapper mapper, IUserProvider userProvider, ICandidateRepository candidateRepository)
        {
            _projectService = projectService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _candidateRepository = candidateRepository;
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

        public async Task BulkAppoinOrCanceltInterviewsAsync(IEnumerable<BulkAppointInterviewsDTO> bulkAppointInterviewsDTO, bool isUpdate)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(bulkAppointInterviewsDTO.Select(c => c.CandidateId));
            var scheduleCandidateInfos = _scheduleRepository.GetScheduleCandidateInfos(candidates);
            var scheduleDict = bulkAppointInterviewsDTO.Select(s => s).ToDictionary(u => u.UserId, u => new ScheduleSlot()
            {
                AvailableTime = u.AppointDateTime,
                ScheduleCandidateInfo = scheduleCandidateInfos.Where(c => c.Id == u.CandidateId).FirstOrDefault(),
            });

            await _scheduleRepository.UpdateOrCancelScheduleCandidateInfosByDictionaryAsync(scheduleDict, isUpdate);
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
    }
}
