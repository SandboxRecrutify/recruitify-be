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

        public Task<Candidate> GetCandidateWithProject(Guid id, Guid projectId)
        {
             var match1 = new BsonDocument("$match", new BsonDocument("_id", id));
             var match2 = new BsonDocument("$unwind", "$ProjectResults");
             var match3 = new BsonDocument("$match", new BsonDocument("ProjectResults.ProjectId", projectId));

             var proroject = new BsonDocument("$project", new BsonDocument(
             "ProjectResults",
             new BsonDocument("Status", 1).Add("Feedbacks", 1).Add("ProjectId", 1).Add("Reason", 1)));
             var aggregatorPipeline = new[] { match1, match2, match3, proroject };
             return GetCollection().Aggregate<Candidate>(aggregatorPipeline).FirstOrDefaultAsync();
        }

        public Task<CandidateStatusFeedBack> GetCandidateWithProjectFeedbackAsync(Guid id, Guid projectId, Guid feedbackUserId, FeedbackType feedbackType)
        {
            return GetCollection().Aggregate().Match(c => c.Id == id)
            .Unwind<Candidate, ProjectResult>(c => c.ProjectResults).Match(p => p.ProjectId == projectId)
             .Project(p =>
                new CandidateStatusFeedBack
                {
                    Status = p.Status,
                    Feedback = p.Feedbacks.Where(f => f.UserId == feedbackUserId && f.Type == feedbackType),
                    ProjectId = p.ProjectId,
                    Reason = p.Reason,
                }).FirstOrDefaultAsync();
        }

        public async Task UpsertFeedbackAsync(Guid id, Guid projectId, Feedback feedback)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
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
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };

            var updateResult = await GetCollection().UpdateOneAsync(filter, updateDefinition, updateOptions);
            if (updateResult.ModifiedCount == 0)
            {
                var projectResultArrayFilters = new List<ArrayFilterDefinition>
                {
                   new BsonDocumentArrayFilterDefinition<ProjectResult>(new BsonDocument("projectResult.ProjectId", binaryProjectId)),
                };
                updateOptions.ArrayFilters = projectResultArrayFilters;
                updateDefinition = updateBuilder
                    .AddToSet("ProjectResults.$[projectResult].Feedbacks", feedback);
                await GetCollection().UpdateOneAsync(filter, updateDefinition, updateOptions);
            }
        }
    }
}
