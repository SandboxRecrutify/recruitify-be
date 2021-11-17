using System;
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
            //var filter = _filterBuilder.ElemMatch(u => u.ProjectRoles,
            //    _filterBuilder.Eq(x => x.ProjectRoles.Keys, Constants.Roles.GlobalProjectId)
            //    );
            var g = Constants.Roles.GlobalProjectId.ToString();
            var filterb =
            _filterBuilder.ElemMatch(
                "ProjectRoles", Builders<KeyValuePair<Guid, IEnumerable<string>>>.Filter.In("v", roles));
            var filter = _filterBuilder.Empty;
            //var filter = _filterBuilder.AnyIn(u => u.ProjectRoles[g], roles);
            //var filterc = _filterBuilder.Eq("ProjectRoles.Key", Constants.Roles.GlobalProjectId);
            var a = GetCollection().Find(filterb).ToListAsync();
            //var q = from doc in _mongoDBCollection.AsQueryable()
            //        where doc.EntityId == accountHolderId
            //        where doc.ActivityDate >= requestDetails.FromDate
            //        where doc.ActivityDate <= requestDetails.ToDate
            //        where doc.UnCommonFields.Any(x => x.k == "ForeignAmount" && new object[] { -10.78, -15.85 }.Contains(x.v))
            //        select doc;
            
            //var a = GetCollection().AsQueryable();
            //var b = a.Where(x => x.ProjectRoles.Any( n => x.ProjectRoles.ContainsKey(Constants.Roles.GlobalProjectId))).ToList();
            //var t = GetCollection().Find(filter).ToListAsync();
            //var a = t.Result;
            //var b = a.Where(x => x.ProjectRoles.Any(n => x.ProjectRoles.ContainsKey(Constants.Roles.GlobalProjectId))).ToList();
            return a;
        }
    }
}
