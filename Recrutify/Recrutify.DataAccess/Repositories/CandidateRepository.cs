﻿using System;
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

        public IQueryable<Candidate> GetCandidatesPassedTestByProject(Guid projectId)
        {
            return GetCollection().AsQueryable().Where(x => x.ProjectResults.Any(p => p.ProjectId == projectId && p.Status == Status.Test));
        }

        public IQueryable<Candidate> GetUnassignedCandidatesByProject(Guid projectId)
        {
            return GetCollection().AsQueryable().Where(x => x.ProjectResults.Any(p => p.ProjectId == projectId
                                                                                 && !p.IsAssignedOnInterview
                                                                                 && (p.Status == Status.RecruiterInterview
                                                                                 || p.Status == Status.TechInterviewOneStep
                                                                                 || p.Status == Status.TechInterviewSecondStep)));
        }

        public Task<CandidatesProjectInfo> CandidatesProjectInfoAsync(Guid? projectId)
        {
            var unwind = new BsonDocument("$unwind", "$ProjectResults");
            var match = projectId.HasValue ? new BsonDocument("$match", new BsonDocument("ProjectResults.ProjectId", new BsonBinaryData(projectId.Value, GuidRepresentation.Standard)))
                : new BsonDocument("$match", new BsonDocument());
            var group = new BsonDocument
            {
               {
                    "$group",  new BsonDocument
               {
                   { "_id", BsonNull.Value },
                   { "Locations", new BsonDocument("$addToSet", "$Location") },
                   { "PrimarySkills", new BsonDocument("$addToSet", "$ProjectResults.PrimarySkill") },
               }
               },
            };
            var pipeline = new[] { unwind, match, group };
            return GetCollection().Aggregate<CandidatesProjectInfo>(pipeline).FirstOrDefaultAsync();
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

        public Task UpdateIsAssignedAsync(IEnumerable<CandidateRenewal> candidateRenewals, Guid projectId)
        {
            var updateBuilder = Builders<Candidate>.Update;
            var updateModels = candidateRenewals.Select(i => new UpdateOneModel<Candidate>(
               _filterBuilder.Eq(c => c.Id, i.CandidateId),
               updateBuilder.Set("ProjectResults.$[projectResults].IsAssignedOnInterview", i.IsAssignedOnInterview).Set("ProjectResults.$[projectResults].Status", i.Status))
            { ArrayFilters = new List<ArrayFilterDefinition>() { new BsonDocumentArrayFilterDefinition<ProjectResult>(new BsonDocument("projectResults.ProjectId", BsonBinaryData.Create(projectId))) } });
            return GetCollection().BulkWriteAsync(updateModels);
        }

        private async Task UpdateWithArrayFiltersAsync(Guid id, UpdateDefinition<Candidate> updateDefinition, List<ArrayFilterDefinition> arrayFilters)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };
            await GetCollection().UpdateOneAsync(filter, updateDefinition, updateOptions);
        }
    }
}
