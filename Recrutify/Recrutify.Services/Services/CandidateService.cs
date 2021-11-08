using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation;
using Recrutify.DataAccess.Extensions;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;
using Recrutify.Services.Validators;

namespace Recrutify.Services.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProjectResult> _validator;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task<List<CandidateDTO>> GetAllAsync()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return _mapper.Map<List<CandidateDTO>>(candidates);
        }

        public async Task<CandidateDTO> GetAsync(Guid id)
        {
            var candidate = await _candidateRepository.GetAsync(id);
            return _mapper.Map<CandidateDTO>(candidate);
        }

        public async Task<CandidateDTO> GetCandidateWithProjectAsync(Guid id, Guid projectId)
        {
            var candidate = await _candidateRepository.GetCandidateWithProject(id, projectId);
            return _mapper.Map<CandidateDTO>(candidate);
        }

        public async Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO)
        {
            var candidate = _mapper.Map<Candidate>(candidateCreateDTO);
            await _candidateRepository.CreateAsync(candidate);
            var result = _mapper.Map<CandidateDTO>(candidate);
            return result;
        }

        public async Task UpsertFeedbackAsync(Guid id, Guid projectId, CreateFeedbackDTO feedbackDto)
        {
            var candidateWithProjectFeedback = await _candidateRepository.GetCandidateWithProjectFeedbackAsync(id, projectId,
                feedbackDto.UserId, _mapper.Map<FeedbackType>(feedbackDto.Type));
            if (candidateWithProjectFeedback == null)
            {
                var newFeedback = _mapper.Map<Feedback>(feedbackDto);
                newFeedback.CreatedOn = DateTime.UtcNow;
                await _candidateRepository.UpsertFeedbackAsync(id, projectId, newFeedback);
            }
            else
            {
              

                var copy = candidateWithProjectFeedback.DeepCopy();
                var result = _mapper.Map<CreateFeedbackDTO>(copy);
                var feedbackToUpdate = _mapper.Map(feedbackDto, result);
                var feedbackToUpdatemap = _mapper.Map<Feedback>(feedbackToUpdate);
                await _candidateRepository.UpsertFeedbackAsync(id, projectId, feedbackToUpdatemap);
            }
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return _candidateRepository.ExistsAsync(id);
        }
    }
}
