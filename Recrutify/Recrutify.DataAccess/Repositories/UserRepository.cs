using System.Collections.Generic;
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

        public Task<User> GetByEmailAsync(string email)
        {
            var filter = _filterBuilder.Eq(u => u.Email, email);
            return GetCollection().Find(filter).FirstOrDefaultAsync();
        }
    }
}
