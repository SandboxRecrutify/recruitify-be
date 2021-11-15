using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
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
                new User
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                }).ToListAsync();
            return users.ToDictionary(k => k.Id, v => $"{v.Name} {v.Surname}");
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var filter = _filterBuilder.Eq(u => u.Email, email);
            return GetCollection().Find(filter).FirstOrDefaultAsync();
        }

        public Task<List<User>> GetByRolesAsync(List<Role> roles)
        {
            var filter = _filterBuilder.AnyIn(u => u.Roles, roles);
            return GetCollection().Find(filter).ToListAsync();
        }
    }
}
