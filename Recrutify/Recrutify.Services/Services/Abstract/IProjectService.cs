using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Services.Abstract
{
    public interface IProjectService
    {
        Task<ProjectDTO> CreateAsync(CreateProjectDTO projectDto);

        Task<ProjectDTO> GetAsync(Guid id);

        Task<List<ProjectDTO>> GetAllAsync();

        IQueryable<ProjectDTO> Get();

        Task<ProjectDTO> UpdateAsync(ProjectDTO projectDto);

        Task DeleteAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);

        Task<PrimarySkillsAndStaffDTO> GetPrimarySkillsAndStaff(List<Role> roles);
    }
}
