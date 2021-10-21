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
        private readonly IProjectRepository _courseRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> CreateAsync(ProjectCreateDTO courseDto)
        {
            var course = _mapper.Map<Project>(courseDto);
            await _courseRepository.CreateAsync(course);

            return _mapper.Map<ProjectDTO>(course);
        }

        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<List<ProjectDTO>>(courses);
        }
    }
}
