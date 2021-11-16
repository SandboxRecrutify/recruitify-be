using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Extensions;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPrimarySkillService _primarySkillService;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper, IUserService userService, IPrimarySkillService primarySkillService)
        {
            _userService = userService;
            _primarySkillService = primarySkillService;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> CreateAsync(CreateProjectDTO projectDto)
        {
            var users = await _userService.GetNamesByIdsAsync(projectDto.Interviewers
                .Union(projectDto.Managers).Union(projectDto.Mentors).Union(projectDto.Recruiters));
            var project = _mapper.Map<Project>(projectDto);
            project.Interviewers = projectDto.Interviewers.GetStaff(users);
            project.Managers = projectDto.Managers.GetStaff(users);
            project.Mentors = projectDto.Mentors.GetStaff(users);
            project.Recruiters = projectDto.Recruiters.GetStaff(users);
            await _projectRepository.CreateAsync(project);

            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<ProjectDTO> GetAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public IQueryable<ShortProjectDTO> GetShort()
        {
            var projects = _projectRepository.Get();
            return _mapper.ProjectTo<ShortProjectDTO>(projects);
        }

        public async Task<IEnumerable<ProjectPrimarySkillDTO>> GetPrimarySkills(Guid id)
        {
            var primarySkills = await _projectRepository.GetPrimarySkills(id);
            return _mapper.Map<IEnumerable<ProjectPrimarySkillDTO>>(primarySkills);
        }

        public IQueryable<ProjectDTO> Get()
        {
            return _mapper.ProjectTo<ProjectDTO>(_projectRepository.Get());
        }

        public async Task<ProjectDTO> UpdateAsync(ProjectDTO projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.UpdateAsync(project);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return _projectRepository.ExistsAsync(id);
        }

        public async Task<PrimarySkillsAndStaffDTO> GetPrimarySkillsAndStaff(List<Role> roles)
        {
            var result = new PrimarySkillsAndStaffDTO()
            {
                PrimarySkills = await _primarySkillService.GetAllAsync(),
                StaffGroup = await _userService.GetStaffByRolesAsync(roles),
            };

            return result;
        }

        public Task IncrementCurrentApplicationsCountAsync(Guid id)
        {
            return _projectRepository.IncrementCurrentApplicationsCountAsync(id);
        }
    }
}
