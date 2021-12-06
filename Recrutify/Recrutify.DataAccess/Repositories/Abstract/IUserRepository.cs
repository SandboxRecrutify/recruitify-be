using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);

        Task<Dictionary<Guid, string>> GetNamesByIdsAsync(IEnumerable<Guid> ids);

        Task<IEnumerable<User>> GetByRoles(IEnumerable<Role> roles);

        Task BulkAddProjectRolesAsync(Guid projectId, IDictionary<Guid, IEnumerable<Role>> usersRoles);

        Task BulkUpdateProjectRolesAsync(Guid projectId, IDictionary<Guid, IEnumerable<Role>> newUsersRoles, IDictionary<Guid, IEnumerable<Role>> removeUsersRoles, IDictionary<Guid, IEnumerable<Role>> updateUsersRoles);

        Task<List<UserShort>> GetUsersShortByIdsAsync(IEnumerable<Guid> ids);
    }
}
