using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IMapper mapper, IUserProvider userProvider, ICandidateService candidateService)
        {
            _projectService = projectService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _candidateService = candidateService;
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

        public async Task BulkAppointOrCancelInterviewsAsync(IEnumerable<InterviewAppointmentDTO> interviewAppointmentDTOs, Guid projectId)
        {
            var candidates = await _candidateService.GetCandidatesByIdsAsync(interviewAppointmentDTOs.Select(c => c.CandidateId));
            var scheduleCandidateInfos = GetScheduleCandidateInfos(candidates, projectId);
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
            await _candidateService.UpdateIsAssignedAndStatusAsync(candidates, projectId);
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

        private IEnumerable<ScheduleCandidateInfoDTO> GetScheduleCandidateInfos(IEnumerable<CandidateDTO> candidates, Guid projectId)
        {
            var scheduleCandidateInfos = new List<ScheduleCandidateInfoDTO>();
            foreach (var candidate in candidates)
            {
                var projectResult = candidate.ProjectResults.FirstOrDefault(pr => pr.ProjectId == projectId);
                if (!projectResult.IsAssigned)
                {
                    var scheduleCandidateInfo = _mapper.Map<ScheduleCandidateInfoDTO>(candidate);
                    scheduleCandidateInfo.ProjectResult = new ScheduleCandidateProjectResultDTO()
                    {
                        IsAssignedOnInterview = true,
                        PrimarySkill = projectResult.PrimarySkill,
                        ProjectId = projectResult.ProjectId,
                        Status = projectResult.Status + 1,
                    };

                    scheduleCandidateInfos.Add(scheduleCandidateInfo);
                }
            }

            return scheduleCandidateInfos;
        }
    }
}
