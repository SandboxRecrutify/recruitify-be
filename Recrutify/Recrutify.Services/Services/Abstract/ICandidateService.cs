using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface ICandidateService
    {
        Task<List<CandidateDTO>> GetAllAsync();

        Task<CandidateDTO> GetAsync(Guid id);

        Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO);

        Task UpsertFeedbackAsync(Guid id, Guid projectId, CreateFeedbackDTO feedbackDto);

        Task<bool> ExistsAsync(Guid id);
    }
}
