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

        public async Task BulkAppoinOrCanceltInterviewsAsync(IEnumerable<AppointInterviewDTO> appointInterviewDTOs)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(appointInterviewDTOs.Where(c => c.CandidateId.HasValue).Select(c => c.CandidateId.Value));
            var scheduleCandidateInfos = GetScheduleCandidateInfos(candidates, appointInterviewDTOs);
            var appointInterviews = appointInterviewDTOs.Select(grp => new AppointInterview()
            {
                ScheduleSlot = new ScheduleSlot()
                {
                    AvailableTime = grp.AppointDateTime,
                    ScheduleCandidateInfo = scheduleCandidateInfos.Where(c => c.Id == grp.CandidateId).FirstOrDefault(),
                },
                UserId = grp.UserId,
                ProjectId = grp.ProjectId,
            }).ToList();

            await _scheduleRepository.UpdateOrCancelScheduleCandidateInfosAsync(appointInterviews);
            await _candidateRepository.UpdateIsAssignedAsync(appointInterviews);
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

        private IEnumerable<ScheduleCandidateInfo> GetScheduleCandidateInfos(IEnumerable<Candidate> candidates, IEnumerable<AppointInterviewDTO> appointInterviewDTOs)
        {
            var scheduleCandidateInfos = new List<ScheduleCandidateInfo>();
            foreach (var candidate in candidates)
            {
                var projectId = appointInterviewDTOs.Where(a => a.CandidateId == candidate.Id).Select(u => u.ProjectId).FirstOrDefault();
                var projectResult = candidate.ProjectResults.Where(x => x.ProjectId == projectId).FirstOrDefault();
                scheduleCandidateInfos.Add(new ScheduleCandidateInfo()
                {
                    BestTimeToConnect = candidate.BestTimeToConnect,
                    Email = candidate.Email,
                    Id = candidate.Id,
                    Name = candidate.Name,
                    ProjectResult = new ScheduleCandidateProjectResult()
                    {
                        IsAssignedOnInterview = true,
                        PrimarySkill = projectResult.PrimarySkill,
                        ProjectId = projectResult.ProjectId,
                        Status = Status.TechInterviewOneStep,
                    },
                    Skype = candidate.Contacts.Where(s => s.Type == Constants.Candidate.Skype).Select(s => s.Value).FirstOrDefault(),
                });
            }

            return scheduleCandidateInfos;
        }
    }
}
