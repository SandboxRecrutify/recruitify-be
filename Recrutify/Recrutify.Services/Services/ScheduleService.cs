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
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public ScheduleService(IProjectService projectService, IScheduleRepository scheduleRepository, IMapper mapper, IUserProvider userProvider, ICandidateRepository candidateRepository, ICandidateService candidateService)
        {
            _projectService = projectService;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
            _candidateRepository = candidateRepository;
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
            var scheduleCandidateInfos = GetScheduleCandidateInfos(candidates, interviewAppointmentDTOs, projectId);
            var appointInterviews = interviewAppointmentDTOs.Select(a => new InterviewAppointment()
            {
                ScheduleSlot = new ScheduleSlot()
                {
                    AvailableTime = a.AppointDateTime,
                    ScheduleCandidateInfo = scheduleCandidateInfos.Where(c => c.Id == a.CandidateId).FirstOrDefault(),
                },
                UserId = a.UserId,
                IsApponint = a.IsAppoint,
            }).ToList();

            await _scheduleRepository.UpdateOrCancelScheduleCandidateInfosAsync(appointInterviews);
            await _candidateRepository.UpdateIsAssignedAsync(appointInterviews, projectId);
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

        private IEnumerable<ScheduleCandidateInfo> GetScheduleCandidateInfos(IEnumerable<CandidateDTO> candidates, IEnumerable<InterviewAppointmentDTO> interviewAppointmentDTOs, Guid projectId)
        {
            var scheduleCandidateInfos = new List<ScheduleCandidateInfo>();
            foreach (var candidate in candidates)
            {
                var projectResult = candidate.ProjectResults.Where(x => x.ProjectId == projectId).FirstOrDefault();
                scheduleCandidateInfos.Add(new ScheduleCandidateInfoDTO()
                {
                    BestTimeToConnect = candidate.BestTimeToConnect,
                    Email = candidate.Email,
                    Id = candidate.Id,
                    Name = candidate.Name,
                    ProjectResult = interviewAppointmentDTOs.Where(a => a.CandidateId == candidate.Id && a.IsAppoint == true).Any() ? new ScheduleCandidateProjectResult()
                    {
                        IsAssignedOnInterview = true,
                        PrimarySkill = projectResult.PrimarySkill,
                        ProjectId = projectId,
                        Status = Status.TechInterviewOneStep,
                    }
                    : new ScheduleCandidateProjectResult(),
                    Skype = candidate.Contacts.Where(s => s.Type == Constants.Contacts.Skype).Select(s => s.Value).FirstOrDefault(),
                });
            }

            return scheduleCandidateInfos;
        }
    }
}
