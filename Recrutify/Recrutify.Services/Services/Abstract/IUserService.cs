using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface IUserService
    {
        public Task<List<StaffDTO>> GetByRoleAsync(Role role);
    }
}
