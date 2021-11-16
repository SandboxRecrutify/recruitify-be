using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;

namespace Recrutify.DataAccess.Repositories.Abstract
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<IEnumerable<ProjectPrimarySkill>> GetPrimarySkills(Guid id);

        Task IncrementCurrentApplicationsCountAsync(Guid id);
    }
}
