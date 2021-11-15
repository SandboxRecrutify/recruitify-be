using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);

        public Task<List<User>> GetByRolesAsync(List<Role> roles);

        Task<Dictionary<Guid, string>> GetNamesByIdsAsync(IEnumerable<Guid> ids);
    }
}
