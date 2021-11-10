using System;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        Task UpdateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task CreateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);
    }
}
