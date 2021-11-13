﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Recrutify.DataAccess.Extensions;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Exceptions;
using Recrutify.Services.Services.Abstract;
using ValidationException = FluentValidation.ValidationException;

namespace Recrutify.Services.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProjectResult> _validator;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper, IValidator<ProjectResult> validator)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
            _validator = validator;
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

        public async Task<CandidateDTO> GetAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetAsync(id);
            return _mapper.Map<CandidateDTO>(candidate);
        }

        public IQueryable<CandidateDTO> Get()
        {
            return _mapper.ProjectTo<CandidateDTO>(_candidateRepository.Get());
        }

        public async Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO)
        {
            var candidate = _mapper.Map<Candidate>(candidateCreateDTO);
            await _candidateRepository.CreateAsync(candidate);
            var result = _mapper.Map<CandidateDTO>(candidate);
            return result;
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

            var projectResult = candidate.ProjectResults?.FirstOrDefault(x => x.ProjectId == projectId);
            var currentFeedback = projectResult?
                .Feedbacks?.FirstOrDefault(x => x.UserId == feedbackDto.UserId && x.Type == _mapper.Map<FeedbackType>(feedbackDto.Type));
            if (currentFeedback == null)
            {
                var newFeedback = _mapper.Map<Feedback>(feedbackDto);
                newFeedback.CreatedOn = DateTime.UtcNow;
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

        public Task<bool> ExistsAsync(Guid id)
        {
            return _candidateRepository.ExistsAsync(id);
        }
    }
}
