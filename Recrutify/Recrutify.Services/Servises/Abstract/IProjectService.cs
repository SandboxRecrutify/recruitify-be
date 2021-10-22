using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.Dtos;

namespace Recrutify.Services.Servises.Abstract
{
    public interface IProjectService
    {
        public Task<ProjectDTO> CreateAsync(ProjectCreateDTO projectDto);

        public Task<List<ProjectDTO>> GetAllAsync();
    }
}
