using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.Dtos;
using Recrutify.Services.Servises.Abstract;

namespace Recrutify.Services.Servises
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> CreateAsync(ProjectCreateDTO projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.CreateAsync(project);

            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            var project = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectDTO>>(project);
        }
    }
}
