using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ICandidateService
    {
        Task<List<CandidateDTO>> GetAllAsync();

        Task<IEnumerable<CandidateDTO>> GetCandidatesByProject(Guid projectId);

        IQueryable<CandidateDTO> Get();

        public Task<CandidateDTO> GetAsync(Guid id);

        Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO);

        Task UpsertFeedbackAsync(Guid id, Guid projectId, CreateFeedbackDTO feedbackDto);

        Task<bool> ExistsAsync(Guid id);
    }
}
