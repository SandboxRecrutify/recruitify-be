using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface IPrimarySkillService
    {
        Task<List<PrimarySkillDTO>> GetAllAsync();
    }
}
