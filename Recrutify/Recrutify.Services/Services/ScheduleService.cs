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
            var candidates = await _candidateRepository.GetByIdsAsync(appointInterviewDTOs.Where(c => c.CandidateId != null).ToList().Select(c => (Guid)c.CandidateId));
            var scheduleCandidateInfos = GetScheduleCandidateInfos(candidates);
            var scheduleDict = appointInterviewDTOs.Select(grp => new AppointInterviewHelper()
            {
                ScheduleSlot = new ScheduleSlot()
            {
                AvailableTime = grp.AppointDateTime,
                ScheduleCandidateInfo = scheduleCandidateInfos.Where(c => c.Id == grp.CandidateId).FirstOrDefault(),
            },
                Candidate = candidates.Where(c => c.Id == grp.CandidateId).FirstOrDefault(),
                UserId = grp.UserId,
            }).ToList();

            await _scheduleRepository.UpdateOrCancelScheduleCandidateInfosAsync(scheduleDict);
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

        private IEnumerable<ScheduleCandidateInfo> GetScheduleCandidateInfos(IEnumerable<Candidate> candidates)
        {
            var scheduleCandidateInfos = new List<ScheduleCandidateInfo>();
            foreach (var candidate in candidates)
            {
                // ProjectResult may be several, how can i choose which i need?
                var projectResult = candidate.ProjectResults.Where(x => x.Status <= Status.TechInterviewOneStep).FirstOrDefault();
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
                        Status = projectResult.Status,
                    },
                    Skype = candidate.Contacts.Where(s => s.Type == "Skype").Select(s => s.Value).FirstOrDefault(),
                });
            }

            return scheduleCandidateInfos;
        }
    }
}
