using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Extensions;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Exceptions;
using Recrutify.Services.Helpers.Abstract;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPrimarySkillService _primarySkillService;
        private readonly IStaffHelper _staffHelper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper, IUserService userService, IPrimarySkillService primarySkillService, IStaffHelper staffHelper)
        {
            _staffHelper = staffHelper;
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
            await _userService.BulkAddProjectRolesAsync(project.Id, _staffHelper.GetStaffUsersByRoles(project));

            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<ProjectDTO> GetAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project == null)
            {
                 throw new NotFoundException();
            }

            return _mapper.Map<ProjectDTO>(project);
        }

        public Task<string> GetProjectName(Guid id)
        {
            return _projectRepository.GetProjectName(id);
        }

        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public IQueryable<ShortProjectDTO> GetShort()
        {
            var projects = _projectRepository.GetShort();
            return _mapper.ProjectTo<ShortProjectDTO>(projects);
        }

        public async Task<IEnumerable<ProjectPrimarySkillDTO>> GetPrimarySkills(Guid id)
        {
            var primarySkills = await _projectRepository.GetPrimarySkills(id);
            return _mapper.Map<IEnumerable<ProjectPrimarySkillDTO>>(primarySkills);
        }

        public IQueryable<ProjectDTO> Get()
        {
            var projects = _projectRepository.Get().OrderByDescending(p => p.IsActive);
            return _mapper.ProjectTo<ProjectDTO>(projects);
        }

        public async Task<ProjectDTO> UpdateAsync(UpdateProjectDTO projectDto)
        {
            var currentProject = await _projectRepository.GetAsync(projectDto.Id);
            var newProject = _mapper.Map(projectDto, currentProject.DeepCopy());
            var users = await _userService.GetNamesByIdsAsync(projectDto.Interviewers
               .Union(projectDto.Managers).Union(projectDto.Mentors).Union(projectDto.Recruiters));
            newProject.Interviewers = projectDto.Interviewers.GetStaff(users);
            newProject.Managers = projectDto.Managers.GetStaff(users);
            newProject.Mentors = projectDto.Mentors.GetStaff(users);
            newProject.Recruiters = projectDto.Recruiters.GetStaff(users);
            await _projectRepository.UpdateAsync(newProject);
            await _userService.BulkUpdateProjectRolesAsync(projectDto.Id, _staffHelper.GetStaffUsersByRoles(currentProject), _staffHelper.GetStaffUsersByRoles(newProject));
            return _mapper.Map<ProjectDTO>(newProject);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return _projectRepository.ExistsAsync(id, cancellationToken);
        }

        public async Task<PrimarySkillsAndStaffDTO> GetPrimarySkillsAndStaff(IEnumerable<Role> roles)
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

        public Task<IEnumerable<Guid>> GetInterviewersIdsAsync(Guid id)
        {
            return _projectRepository.GetInterviewersIdsAsync(id);
        }
    }
}
