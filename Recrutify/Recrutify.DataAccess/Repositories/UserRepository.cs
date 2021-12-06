using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<MongoSettings> options)
           : base(options)
        {
        }

        public async Task<Dictionary<Guid, string>> GetNamesByIdsAsync(IEnumerable<Guid> ids)
        {
            var filter = _filterBuilder.In(u => u.Id, ids);
            var users = await GetCollection().Find(filter).Project(u =>
                new
                {
                    u.Id,
                    u.Name,
                    u.Surname,
                }).ToListAsync();
            return users.ToDictionary(u => u.Id, u => $"{u.Name} {u.Surname}");
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var filter = _filterBuilder.Eq(u => u.Email, email);
            return GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetByRoles(IEnumerable<Role> roles)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder
                            .ElemMatch(
                                nameof(User.ProjectRoles),
                                filterBuilder.And(filterBuilder.Eq("k", Constants.GlobalProject.GlobalProjectId)) & filterBuilder.AnyIn("v", roles));
            var users = await GetBsonDocumentCollection().Find(filter).ToListAsync();
            return BsonSerializer.Deserialize<IEnumerable<User>>(users.ToJson());
        }

        public Task BulkAddProjectRolesAsync(Guid projectId, IDictionary<Guid, IEnumerable<Role>> usersRoles)
        {
            var updateModels = GetUpdateModelsForAddingRoles(projectId, usersRoles);
            return GetCollection().BulkWriteAsync(updateModels);
        }

        public Task BulkUpdateProjectRolesAsync(Guid projectId, IDictionary<Guid, IEnumerable<Role>> newUsersRoles, IDictionary<Guid, IEnumerable<Role>> removeUsersRoles, IDictionary<Guid, IEnumerable<Role>> updateUsersRoles)
        {
            var binaryProjectId = new BsonBinaryData(projectId, GuidRepresentation.Standard);
            var arrayFilters = new List<ArrayFilterDefinition>
            {
               new BsonDocumentArrayFilterDefinition<ProjectResult>(new BsonDocument("keyValuePair.k", binaryProjectId)),
            };
            var updateBuilder = Builders<User>.Update;

            var updateModelsWithNewUsers = GetUpdateModelsForAddingRoles(projectId, newUsersRoles);

            var updateModelsWithRemovedUsers = removeUsersRoles.Select(ur => new UpdateOneModel<User>(
                                                    _filterBuilder.Eq(u => u.Id, ur.Key),
                                                    updateBuilder.PullFilter(p => p.ProjectRoles, new BsonDocument("k", binaryProjectId))));

            var updateModelsWithUpdateUsers = updateUsersRoles.Select(ur => new UpdateOneModel<User>(
                                                    _filterBuilder.Eq(u => u.Id, ur.Key),
                                                    updateBuilder
                                                    .Set(
                                                      $"{nameof(User.ProjectRoles)}.$[keyValuePair]",
                                                      ur.Value)) { ArrayFilters = arrayFilters });

            return GetCollection().BulkWriteAsync(updateModelsWithNewUsers.Union(updateModelsWithRemovedUsers).Union(updateModelsWithUpdateUsers));
        }

        public Task<List<UserShort>> GetUsersShortByIdsAsync(IEnumerable<Guid> ids)
        {
            var filter = _filterBuilder.In(u => u.Id, ids);
            return GetCollection().Find(filter).Project(u =>
                                                            new UserShort
                                                            {
                                                                Id = u.Id,
                                                                Name = u.Name,
                                                                Email = u.Email,
                                                            }).ToListAsync();
        }

        private IEnumerable<UpdateOneModel<User>> GetUpdateModelsForAddingRoles(Guid projectId, IDictionary<Guid, IEnumerable<Role>> usersRoles)
        {
            var updateBuilder = Builders<User>.Update;
            return usersRoles.Select(ur => new UpdateOneModel<User>(
                                                    _filterBuilder.Eq(u => u.Id, ur.Key),
                                                    updateBuilder
                                                    .AddToSet(
                                                        nameof(User.ProjectRoles),
                                                        new KeyValuePair<Guid, IEnumerable<Role>>(
                                                                projectId,
                                                                ur.Value))));
        }
    }
}
