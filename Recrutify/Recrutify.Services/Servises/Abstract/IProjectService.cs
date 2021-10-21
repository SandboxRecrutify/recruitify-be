using System.Collections.Generic;
using System.Threading.Tasks;
using Recrutify.Services.Dtos;

namespace Recrutify.Services.Servises.Abstract
{
    public interface IProjectService
    {
        public Task<ProjectDto> CreateAsync(ProjectCreateDto courseDto);

        public Task<List<ProjectDto>> GetAllAsync();
    }
}
