using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(IOptions<MongoSettings> options)
            : base(options)
        {
        }

        public async Task UpserAsync(Guid id, Guid projectId, Feedback feedback)
        {
            var filter = _filterBuilder.And(
                _filterBuilder.Eq(x => x.Id, id),
                _filterBuilder.ElemMatch(p => p.ProjectResults, x => x.ProjectId == projectId),
                _filterBuilder.Where(c => c.ProjectResults[-1].Feedbacks
                          .All(f => f.UserId.Equals(feedback.UserId) && f.Type.Equals(feedback.Type))));
            var update = Builders<Candidate>.Update
                 .Set(c => c.ProjectResults[-1].Feedbacks[-1], feedback);
            var result = await GetCollection().UpdateOneAsync(filter, update);
            if (result.ModifiedCount == 0)
            {
                update = Builders<Candidate>.Update
                    .AddToSet(x => x.ProjectResults[-1].Feedbacks, feedback);
                await GetCollection().UpdateOneAsync(filter, update);
            }
        }
    }
}
