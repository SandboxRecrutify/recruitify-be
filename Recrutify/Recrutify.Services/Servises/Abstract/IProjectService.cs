using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.DTOs;

namespace Recrutify.Services.Servises.Abstract
{
    public interface IProjectService
    {
        Task<ProjectDTO> CreateAsync(CreateProjectDTO projectDto);

        Task<ProjectDTO> GetAsync(Guid id);

        Task<List<ProjectDTO>> GetAllAsync();

        Task<ProjectDTO> UpdateAsync(ProjectDTO projectDto);

        Task DeleteAsync(Guid id);
    }
}
