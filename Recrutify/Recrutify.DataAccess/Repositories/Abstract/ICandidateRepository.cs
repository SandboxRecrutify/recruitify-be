using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        Task<List<Candidate>> GetByProjectAsync(Guid projectId);

        Task UpdateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task CreateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task<Candidate> GetByEmailAsync(string email);

        Task ReplaceAsync(Candidate candidate);
    }
}
