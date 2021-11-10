using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        Task UpsertFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task<List<Candidate>> GetByProjectAsync(Guid projectId);
    }
}
