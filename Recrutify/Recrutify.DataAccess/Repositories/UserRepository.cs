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
                                filterBuilder
                                    .And(filterBuilder.Eq("k", Constants.GlobalProject.GlobalProjectId)) &
                                         filterBuilder.AnyIn("v", roles));
            var users = await GetBsonDocumentCollection().Find(filter).ToListAsync();
            return BsonSerializer.Deserialize<IEnumerable<User>>(users.ToJson());
        }

        public async Task CreateStaffByProject(StaffByProject staffByProject)
        {
            var usersAll = staffByProject.Interviewers.Select(x => new { UserId = x.UserId, Role = Role.Interviewer })
                    .Union(staffByProject.Managers.Select(x => new { UserId = x.UserId, Role = Role.Manager }))
                    .Union(staffByProject.Recruiters.Select(x => new { UserId = x.UserId, Role = Role.Recruiter }))
                    .Union(staffByProject.Mentors.Select(x => new { UserId = x.UserId, Role = Role.Mentor }));
            var userGroups = usersAll.GroupBy(o => o.UserId).ToDictionary(a => a.Key, a => a.Select(o => o.Role).ToList());
            var updateManyDB = new List<WriteModel<User>>();
            foreach (var e in userGroups)
            {
                var updateBuilder = Builders<User>.Update;
                var updateDefinition = updateBuilder
                    .AddToSet("ProjectRoles", new KeyValuePair<Guid, IEnumerable<Role>>(staffByProject.ProjectId, e.Value));
                updateManyDB.Add(new UpdateManyModel<User>(_filterBuilder.Eq(x => x.Id, e.Key), updateDefinition));
            }

            await GetCollection().BulkWriteAsync(updateManyDB);
        }
    }
}
