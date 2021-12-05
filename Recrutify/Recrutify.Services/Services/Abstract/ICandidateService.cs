﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<CandidateDTO>> GetCandidatesByIdsAsync(IEnumerable<Guid> ids);

        Task<CandidateDTO> GetCandidateWithProjectAsync(Guid id, Guid projectId);

        Task BulkCreateTestFeedbacksAsync(BulkCreateTestFeedbackDTO bulkCreateTestFeedbackDTO, Guid projectId);

        Task BulkUpdateStatusReasonAsync(BulkUpdateStatusDTO bulkUpdateStatusDTO, Guid projectId);

        IEnumerable<ScheduleCandidateInfoDTO> GetCandidatesPassedTest(Guid projectId);

        IEnumerable<ScheduleCandidateInfoDTO> GetUnassignedCandidates(Guid projectId);

        Task BulkSendEmailsWithTestAsync(BulkSendEmailWithTestDTO bulkSendEmailWithTestDTO, Guid projectId);

        Task<CandidatesProjectInfoDTO> GetCandidatesProjectInfoAsync(Guid? projectId);

        Task UpdateIsAssignedAndStatusAsync(IEnumerable<CandidateDTO> candidateDTOs, Guid projectId);
    }
}
