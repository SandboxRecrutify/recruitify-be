using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ICandidateService
    {
        public Task<List<CandidateDTO>> GetAllAsync();

        IQueryable<CandidateDTO> Get();

        public Task<CandidateDTO> GetAsync(Guid id);

        public Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO);

        Task UpsertAsync(Guid id, Guid projectId, FeedbackDTO feedbackDto);

        Task<bool> ExistsAsync(Guid id);
    }
}
