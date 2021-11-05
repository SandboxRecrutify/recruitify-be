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

        /* public Task<Candidate> GetCandidateWithProjectFeedbackAsync(Guid id, Guid projectId, Guid feedbackUserId, FeedbackType feedbackType)
         {
              var binaryId = new BsonBinaryData(id, GuidRepresentation.Standard);
              var binaryProjectId = new BsonBinaryData(projectId, GuidRepresentation.Standard);
              var binaryFeedbackUserId = new BsonBinaryData(feedbackUserId, GuidRepresentation.Standard);
             /* var aggregatorPipeline = new BsonDocument[]
              {
                    new BsonDocument { { "$match", new BsonDocument("_id", binaryId) } },
                    new BsonDocument { { "$unwind", "$ProjectResults" } },
                    new BsonDocument { { "$match", new BsonDocument("ProjectResults.ProjectId", binaryProjectId) } },
                    new BsonDocument
                      {
                          {
                              "$project", new BsonDocument
                              {
                              {
                                 "ProjectResults", new BsonDocument
                                 {
                                    { "Status", 1 },
                                    {
                                    "Feedbacks", new BsonDocument
                                    {
                                      {
                                         "$filter", new BsonDocument{
                                             { "input", "$ProjectResults.Feedbacks"},
                                             { "as", "feedback" },
                                             { "cond", new BsonDocument(
                                             }
                                      },
                                    }
                                    },
                                 }
                              },
                              }
                          },
                      },
              };*/

        /* var match1 = new BsonDocument( "$match", new BsonDocument("_id", binaryId));
         var match2 = new BsonDocument( "$unwind", "$ProjectResults");
         var match3 = new BsonDocument( "$match", new BsonDocument("ProjectResults.ProjectId", binaryProjectId));
         var proroject = new BsonDocument("$project", new BsonDocument(
             "ProjectResults",
             new BsonDocument("Status", 1).Add("Feedbacks",
                 new BsonDocument("$filter",
            new BsonDocument("input", "$ProjectResults.Feedbacks").Add("as", "feedback").Add("cond",
               new BsonDocument("$and", new BsonArray
            {
               new BsonDocument("$eq", new BsonArray().Add("$$feedback.UserId").Add(binaryFeedbackUserId)),
               new BsonDocument("$eq", new BsonArray().Add("$$feedback.Type").Add(feedbackType)),
            }))))));
        var aggregatorPipeline = new[] { match1, match2, match3, proroject};
        return GetCollection().Aggregate().Match(c => c.Id == id)
            .Unwind("ProjectResults")
            .Match(new BsonDocument() { { "ProjectResults.ProjectId", projectId } })
            .Project(new BsonDocument()
                {
                    { "Status", "$ProjectResults.Status" },
                    { "Feedbacks", "$Feedbacks.Nome" },
                    { "CNPJ", "$Clientes.CNPJ" },
                    { "CPF", "$Clientes.CPF" },
                    { "Tokens", "$Clientes.Tokens" }
                });
           .Unwind<Candidate, ProjectResult>(c => c.ProjectResults).Match(p => p.ProjectId == projectId)
            .Project(p =>
               new {
                  Status = p.Status,
                  Feedback = p.Feedbacks.FirstOrDefault(f => f.UserId == feedbackUserId && f.Type == feedbackType)
                   }).FirstOrDefaultAsync();
        return GetCollection().Aggregate<Candidate>(aggregatorPipeline).FirstOrDefaultAsync();

    }*/
        /*public Task<Candidate> GetCandidateWithProjectFeedbackAsync(Guid id, Guid projectId, Guid feedbackUserId, FeedbackType feedbackType)
        {
            var binaryId = new BsonBinaryData(id, GuidRepresentation.Standard);
            var binaryProjectId = new BsonBinaryData(projectId, GuidRepresentation.Standard);
            var binaryFeedbackUserId = new BsonBinaryData(feedbackUserId, GuidRepresentation.Standard);
            var match1 = new BsonDocument("$match", new BsonDocument("_id", binaryId));
            var match2 = new BsonDocument("$unwind", "$ProjectResults");
            var filter = new BsonDocument {
                            { "input", "$ProjectResults.Feedbacks"},
                            {"as", "feedback" },
                            {"cond", new BsonDocument {
                                                        { "$and",  new BsonArray { "$$feedback.UserId", binaryFeedbackUserId }} }
                            }
            };
            var project = new BsonDocument {
                          { "ProjectResult", new BsonDocument {
                              {"Status", 1 },
                              {"Feedbacks",  filter }
                          }
                }
            };
            var pipeline = new[] { match1, match2, filter, project };
            return GetCollection().Aggregate<Candidate>(pipeline).FirstOrDefaultAsync();
        }*/

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
