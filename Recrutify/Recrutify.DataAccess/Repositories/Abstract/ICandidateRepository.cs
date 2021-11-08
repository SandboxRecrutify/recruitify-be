using System;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        Task UpsertFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task<ProjectResult> GetCandidateWithProject(Guid id, Guid projectId);

        Task<ProjectResult> GetCandidateWithProjectFeedbackAsync(Guid id, Guid projectId, Guid feedbackUserId, FeedbackType feedbackType);
    }
}
