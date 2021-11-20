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
            var updateBuilder = Builders<User>.Update;
            var updateManyDB = usersRoles.Select(ur => new UpdateOneModel<User>(
                                                    _filterBuilder.Eq(u => u.Id, ur.Key),
                                                    updateBuilder
                                                    .AddToSet(
                                                        "ProjectRoles",
                                                        new KeyValuePair<Guid, IEnumerable<Role>>(
                                                                projectId,
                                                                ur.Value))));

            return GetCollection().BulkWriteAsync(updateManyDB);
        }
    }
}
