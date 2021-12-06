using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Recrutify.DataAccess.Extensions;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Events;
using Recrutify.Services.Events.Abstract;
using Recrutify.Services.Exceptions;
using Recrutify.Services.Providers;
using Recrutify.Services.Services.Abstract;
using ValidationException = FluentValidation.ValidationException;

namespace Recrutify.Services.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IValidator<ProjectResult> _validator;
        private readonly IUserProvider _userProvider;
        private readonly IUpdateStatusEventPublisher _updateStatusEventPublisher;
        private readonly ISendEmailQueueService _sendQueueEmailService;
        private readonly IPrimarySkillService _primarySkillService;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper, IValidator<ProjectResult> validator, IProjectService projectService, IUserProvider userProvider, IUpdateStatusEventPublisher updateStatusEventPublisher, ISendEmailQueueService sendEmailQueueService, IPrimarySkillService primarySkillService)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
            _validator = validator;
            _projectService = projectService;
            _userProvider = userProvider;
            _updateStatusEventPublisher = updateStatusEventPublisher;
            _sendQueueEmailService = sendEmailQueueService;
            _primarySkillService = primarySkillService;
        }

        public async Task<List<CandidateDTO>> GetAllAsync()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return _mapper.Map<List<CandidateDTO>>(candidates);
        }

        public IQueryable<CandidateDTO> GetByProject(Guid projectId)
        {
            var candidates = _candidateRepository.GetByProject(projectId);
            return _mapper.ProjectTo<CandidateDTO>(candidates);
        }

        public IEnumerable<ScheduleCandidateInfoDTO> GetCandidatesPassedTest(Guid projectId)
        {
            var candidates = _candidateRepository.GetCandidatesPassedTestByProject(projectId);
            var candidatesDtos = _mapper.ProjectTo<ScheduleCandidateInfoDTO>(candidates).ToList();
            foreach (var candidateDto in candidatesDtos)
            {
                var candidate = candidates.FirstOrDefault(c => c.Id == candidateDto.Id);
                candidateDto.ProjectResult = _mapper.Map<ScheduleCandidateProjectResultDTO>(candidate.ProjectResults.FirstOrDefault(p => p.ProjectId == projectId));
            }

            return candidatesDtos;
        }

        public IEnumerable<ScheduleCandidateInfoDTO> GetUnassignedCandidates(Guid projectId)
        {
            var candidates = _candidateRepository.GetUnassignedCandidatesByProject(projectId);
            var candidatesDtos = _mapper.ProjectTo<ScheduleCandidateInfoDTO>(candidates).ToList();
            foreach (var candidateDto in candidatesDtos)
            {
                var candidate = candidates.FirstOrDefault(c => c.Id == candidateDto.Id);
                candidateDto.ProjectResult = _mapper.Map<ScheduleCandidateProjectResultDTO>(candidate.ProjectResults.FirstOrDefault(p => p.ProjectId == projectId));
            }

            return candidatesDtos;
        }

        public async Task<CandidateDTO> GetAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetAsync(id);
            return _mapper.Map<CandidateDTO>(candidate);
        }

        public IQueryable<CandidateDTO> Get()
        {
            return _mapper.ProjectTo<CandidateDTO>(_candidateRepository.Get());
        }

        public async Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO, Guid projectId)
        {
            var candidate = _mapper.Map<Candidate>(candidateCreateDTO);
            var currentCandidate = await _candidateRepository.GetByEmailAsync(candidate.Email);
            var currentPrimarySkill = await _primarySkillService.GetAsync(candidateCreateDTO.PrimarySkillId);
            if (currentPrimarySkill == null)
            {
                throw new ValidationException("Primary skill does't exist!");
            }

            var primarySkill = _mapper.Map<CandidatePrimarySkill>(new CandidatePrimarySkillDTO { Id = candidateCreateDTO.PrimarySkillId, Name = currentPrimarySkill.Name });
            var projectResults = new List<ProjectResult> { new ProjectResult { ProjectId = projectId, PrimarySkill = primarySkill, Feedbacks = new List<Feedback> { } } };

            if (currentCandidate == null)
            {
                candidate.Id = Guid.NewGuid();
                candidate.ProjectResults = projectResults;
                await _candidateRepository.CreateAsync(candidate);
                var newCanditate = _mapper.Map<CandidateDTO>(candidate);
                await _projectService.IncrementCurrentApplicationsCountAsync(projectId);
                return newCanditate;
            }

            var candidateToUpdate = _mapper.Map(candidateCreateDTO, currentCandidate.DeepCopy());
            var currentProjectResults = currentCandidate.ProjectResults?.ToList();
            var newProjectResult = new ProjectResult { ProjectId = projectId, PrimarySkill = primarySkill };
            if (currentProjectResults == null)
            {
                currentProjectResults = new List<ProjectResult> { newProjectResult };
            }
            else
            {
                currentProjectResults.Add(newProjectResult);
            }

            candidateToUpdate.ProjectResults = currentProjectResults;

            await _candidateRepository.ReplaceAsync(candidateToUpdate);
            return _mapper.Map<CandidateDTO>(candidateToUpdate);
        }

        public async Task<CandidateDTO> GetCandidateWithProjectAsync(Guid id, Guid projectId)
        {
            var candidate = await _candidateRepository.GetAsync(id);
            if (candidate == null)
            {
                throw new NotFoundException();
            }

            var currentProjectResult = candidate.ProjectResults?.FirstOrDefault(x => x.ProjectId == projectId);
            candidate.ProjectResults = new List<ProjectResult> { currentProjectResult };
            return _mapper.Map<CandidateDTO>(candidate);
        }

        public async Task UpsertFeedbackAsync(Guid id, Guid projectId, UpsertFeedbackDTO feedbackDto)
        {
            var candidate = await _candidateRepository.GetAsync(id);
            if (candidate == null)
            {
                throw new NotFoundException();
            }

            var userId = _userProvider.GetUserId();
            var projectResult = candidate.ProjectResults?.FirstOrDefault(x => x.ProjectId == projectId);
            var currentFeedback = projectResult?
                .Feedbacks?.FirstOrDefault(x => x.UserId == userId && x.Type == _mapper.Map<FeedbackType>(feedbackDto.Type));
            if (currentFeedback == null)
            {
                var newFeedback = _mapper.Map<Feedback>(feedbackDto);
                newFeedback.CreatedOn = DateTime.UtcNow;
                newFeedback.UserId = userId;
                newFeedback.UserName = _userProvider.GetUserName();
                await _candidateRepository.CreateFeedbackAsync(id, projectId, newFeedback);
            }
            else
            {
                var feedbackToUpdate = _mapper.Map(feedbackDto, currentFeedback.DeepCopy());
                var validationResult = await _validator.ValidateAsync(new ProjectResult
                                                                         {
                                                                            Status = projectResult.Status,
                                                                            Feedbacks = new List<Feedback> { feedbackToUpdate },
                                                                         });
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                await _candidateRepository.UpdateFeedbackAsync(id, projectId, feedbackToUpdate);
            }
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return _candidateRepository.ExistsAsync(id, cancellationToken);
        }

        public Task BulkCreateTestFeedbacksAsync(BulkCreateTestFeedbackDTO bulkCreateTestFeedbackDTO, Guid projectId)
        {
            return _candidateRepository.CreateFeedbacksByIdsAsync(
                bulkCreateTestFeedbackDTO.CandidatesIds,
                projectId,
                new Feedback()
                {
                    UserName = _userProvider.GetUserName(),
                    CreatedOn = DateTime.UtcNow,
                    Rating = bulkCreateTestFeedbackDTO.Rating,
                    UserId = _userProvider.GetUserId(),
                    Type = FeedbackType.Test,
                });
        }

        public Task BulkUpdateStatusReasonAsync(BulkUpdateStatusDTO bulkUpdateStatusDTO, Guid projectId)
        {
            _updateStatusEventPublisher.OnStatusUpdated(new UpdateStatusEventArgs() { CandidatesIds = bulkUpdateStatusDTO.CandidatesIds, CandidateStatus = bulkUpdateStatusDTO.Status, ProjectId = bulkUpdateStatusDTO.ProjectId });
            return _candidateRepository.UpdateStatusByIdsAsync(bulkUpdateStatusDTO.CandidatesIds, projectId, _mapper.Map<Status>(bulkUpdateStatusDTO.Status), bulkUpdateStatusDTO.Reason);
        }

        public async Task<IEnumerable<CandidateDTO>> GetCandidatesDTOByIdsAsync(IEnumerable<Guid> ids)
        {
            var candidates = await _candidateRepository.GetByIdsAsync(ids);
            return _mapper.Map<List<CandidateDTO>>(candidates);
        }

        public async Task BulkSendEmailsWithTestAsync(BulkSendEmailWithTestDTO bulkSendEmailWithTestDTO, Guid projectId)
        {
            var candidates = await GetCandidatesDTOByIdsAsync(bulkSendEmailWithTestDTO.CandidatesIds);
            var project = await _projectService.GetAsync(projectId);
            await _candidateRepository.BulkUpdateStatusAsync(bulkSendEmailWithTestDTO.CandidatesIds, project.Id, Status.Test);
            _sendQueueEmailService.SendEmailQueueForTest(candidates, project, bulkSendEmailWithTestDTO.TestDeadlineDate, bulkSendEmailWithTestDTO.PersonToContactEmail);
        }

        public async Task<CandidatesProjectInfoDTO> GetCandidatesProjectInfoAsync(Guid? projectId)
        {
           var primarySkillsAddLocations = await _candidateRepository.CandidatesProjectInfoAsync(projectId);
           var candidatesProjectInfo = _mapper.Map<CandidatesProjectInfoDTO>(primarySkillsAddLocations);
           var projectName = projectId.HasValue ? await _projectService.GetProjectName(projectId.Value) : null;
           candidatesProjectInfo.ProjectName = projectName;
           return candidatesProjectInfo;
        }

        public Task BulkUpdateStatusAsync(IEnumerable<Guid> candidatesIds, Guid projectId, Status status)
        {
            return _candidateRepository.BulkUpdateStatusAsync(candidatesIds, projectId, status);
        }

        public Task<List<Candidate>> GetCandidatesByIdsAsync(IEnumerable<Guid> ids)
        {
            return _candidateRepository.GetByIdsAsync(ids);
        }
    }
}
