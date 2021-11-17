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
    }
}
