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

        IQueryable<CandidateDTO> GetByProject(Guid projectId);

        IQueryable<CandidateDTO> Get();

        public Task<CandidateDTO> GetAsync(Guid id);

        Task<CandidateDTO> CreateAsync(CandidateCreateDTO candidateCreateDTO, Guid projectId);

        Task UpsertFeedbackAsync(Guid id, Guid projectId, UpsertFeedbackDTO feedbackDto);

        Task<bool> ExistsAsync(Guid id);

        Task<CandidateDTO> GetCandidateWithProjectAsync(Guid id, Guid projectId);

        Task BulkCreateTestFeedbacksAsync(BulkCreateTestFeedbackDTO bulkCreateTestFeedbackDTO, Guid projectId);

        Task BulkUpdateStatusReasonAsync(BulkUpdateStatusDTO bulkUpdateStatusDTO, Guid projectId);
    }
}
