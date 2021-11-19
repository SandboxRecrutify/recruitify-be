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

        Task<IEnumerable<User>> GetByRoles(List<Role> roles);
    }
}
