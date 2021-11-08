﻿using System;
using System.Collections.Generic;
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

        public Task<List<Candidate>> GetCandidatesByProject(Guid projectId)
        {
            var filter = _filterBuilder.ElemMatch(x => x.ProjectResults, x => x.ProjectId == projectId);
            return GetCollection().Find(filter).ToListAsync();
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
