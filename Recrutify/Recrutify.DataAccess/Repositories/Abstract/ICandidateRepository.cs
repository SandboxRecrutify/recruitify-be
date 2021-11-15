﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        IQueryable<Candidate> GetByProject(Guid projectId);

        Task UpdateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task CreateFeedbackAsync(Guid id, Guid projectId, Feedback feedback);

        Task<Candidate> GetByEmailAsync(string email);

        Task ReplaceAsync(Candidate candidate);
        IEnumerable<Candidate> GetManyCandidates(IEnumerable<Guid> candidatesId);
        Task CreateFeedbackByManyCandidatesAsync(IEnumerable<Guid> id, Guid projectId, Feedback feedback);
    }
}
