using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

        public IQueryable<Candidate> GetByProject(Guid projectId)
        {
            return GetCollection().AsQueryable().Where(x => x.ProjectResults.Any(y => y.ProjectId == projectId));
        }

        public Task<Candidate> GetByEmailAsync(string email)
        {
            var filter = _filterBuilder.Eq(c => c.Email, email);
            return GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public Task<List<Candidate>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var filter = _filterBuilder.In(u => u.Id, ids);
            return GetCollection().Find(filter).ToListAsync();
        }

        public Task ReplaceAsync(Candidate candidate)
        {
            var filter = _filterBuilder.Eq(c => c.Email, candidate.Email);
            return GetCollection().ReplaceOneAsync(filter, candidate);
        }

        public Task UpdateFeedbackAsync(Guid id, Guid projectId, Feedback feedback)
        {
            var updateBuilder = Builders<Candidate>.Update;
            var updateDefinition = updateBuilder
                 .Set("ProjectResults.$[projectResult].Feedbacks.$[feedback]", feedback);
            var binaryProjectId = new BsonBinaryData(projectId, GuidRepresentation.Standard);
            var binaryFeedbackUserId = new BsonBinaryData(feedback.UserId, GuidRepresentation.Standard);
            var arrayFilters = new List<ArrayFilterDefinition>
            {
               new BsonDocumentArrayFilterDefinition<ProjectResult>(new BsonDocument("projectResult.ProjectId", binaryProjectId)),
               new BsonDocumentArrayFilterDefinition<Feedback>(new BsonDocument("$and", new BsonArray
                {
                   new BsonDocument("feedback.UserId", binaryFeedbackUserId),
                   new BsonDocument("feedback.Type", feedback.Type),
                })),
            };

            return UpdateWithArrayFiltersAsync(id, updateDefinition, arrayFilters);
        }

        public Task CreateFeedbackAsync(Guid id, Guid projectId, Feedback feedback)
        {
            var updateBuilder = Builders<Candidate>.Update;
            var updateDefinition = updateBuilder
                    .AddToSet("ProjectResults.$[projectResult].Feedbacks", feedback);
            var binaryProjectId = new BsonBinaryData(projectId, GuidRepresentation.Standard);
            var arrayFilters = new List<ArrayFilterDefinition>
                {
                   new BsonDocumentArrayFilterDefinition<ProjectResult>(new BsonDocument("projectResult.ProjectId", binaryProjectId)),
                };
            return UpdateWithArrayFiltersAsync(id, updateDefinition, arrayFilters);
        }

        public Task CreateFeedbacksByIdsAsync(IEnumerable<Guid> ids, Guid projectId, Feedback feedback)
        {
            var filter = _filterBuilder.In(x => x.Id, ids);

            var updateBuilder = Builders<Candidate>.Update;
            var updateDefinition = updateBuilder
                    .AddToSet("ProjectResults.$[projectResult].Feedbacks", feedback);
            var binaryProjectId = new BsonBinaryData(projectId, GuidRepresentation.Standard);
            var arrayFilters = new List<ArrayFilterDefinition>
                {
                   new BsonDocumentArrayFilterDefinition<ProjectResult>(new BsonDocument("projectResult.ProjectId", binaryProjectId)),
                };

            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };

            return GetCollection().UpdateManyAsync(filter, updateDefinition, updateOptions);
        }

        public Task UpdateStatusByIdsAsync(IEnumerable<Guid> ids, Guid projectId, Status status, string reason)
        {
            var filter = _filterBuilder.In(x => x.Id, ids);
            var updateBuilder = Builders<Candidate>.Update;
            var updateDefinition = updateBuilder
                    .Set("ProjectResults.$[projectResult].Status", status).Set("ProjectResults.$[projectResult].Reason", reason);
            var binaryProjectId = new BsonBinaryData(projectId, GuidRepresentation.Standard);
            var arrayFilters = new List<ArrayFilterDefinition>
                {
                   new BsonDocumentArrayFilterDefinition<ProjectResult>(new BsonDocument("projectResult.ProjectId", binaryProjectId)),
                };

            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };

            return GetCollection().UpdateManyAsync(filter, updateDefinition, updateOptions);
        }

        private async Task UpdateWithArrayFiltersAsync(Guid id, UpdateDefinition<Candidate> updateDefinition, List<ArrayFilterDefinition> arrayFilters)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            await GetCollection().UpdateOneAsync(filter, updateDefinition, updateOptions);
        }
    }
}
