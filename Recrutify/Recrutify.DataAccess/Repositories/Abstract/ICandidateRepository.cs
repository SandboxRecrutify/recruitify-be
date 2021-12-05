using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        IQueryable<Candidate> GetByProject(Guid projectId);

        IQueryable<Candidate> GetCandidatesPassedTestByProject(Guid projectId);

        IQueryable<Candidate> GetUnassignedCandidatesByProject(Guid projectId);

        Task UpdateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task CreateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task<Candidate> GetByEmailAsync(string email);

        Task ReplaceAsync(Candidate candidate);

        Task<List<Candidate>> GetByIdsAsync(IEnumerable<Guid> ids);

        Task CreateFeedbacksByIdsAsync(IEnumerable<Guid> ids, Guid projectId, Feedback feedback);

        Task UpdateStatusByIdsAsync(IEnumerable<Guid> ids, Guid projectId, Status status, string reason);

        Task<CandidatesProjectInfo> CandidatesProjectInfoAsync(Guid? projectId);

        Task BulkUpdateStatusAsync(IEnumerable<Guid> candidatesIds, Guid projectId, Status status);
    }
}
