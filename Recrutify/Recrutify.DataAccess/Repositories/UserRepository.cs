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

        public Task<List<User>> GetByRolesAsync(List<Role> roles)
        {


            var filter = _filterBuilder.ElemMatch(u => u.ProjectRoles, x => x.Key == Constants.Roles.GlobalProjectId);

            //var filter = _filterBuilder.And(
            //   _filterBuilder.ElemMatch(
            //       x => x.ProjectRoles,
            //       _filterBuilder.And(
            //         _filterBuilder.Eq(x => x.ProjectRoles.Keys, Constants.Roles.GlobalProjectId),
            //         _filterBuilder.AnyIn(x => x.ProjectRoles.Values, roles)
            //    )));
            var a = GetCollection().Find(filter).ToListAsync();
            return a;
        }
    }
}
