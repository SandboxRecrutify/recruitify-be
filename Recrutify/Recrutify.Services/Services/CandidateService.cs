using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

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

        public async Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO)
        {
            var candidate = _mapper.Map<Candidate>(candidateCreateDTO);
            await _candidateRepository.CreateAsync(candidate);
            var result = _mapper.Map<CandidateDTO>(candidate);
            return result;
        }

        public Task UpsertAsync(Guid id, Guid projectId, FeedbackDTO feedbackDto)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDto);
            return _candidateRepository.UpsertFeedbackAsync(id, projectId, feedback);
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return _candidateRepository.ExistsAsync(id);
        }
    }
}
