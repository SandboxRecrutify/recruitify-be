﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
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

        public async Task<ProjectDTO> CreateAsync(CreateProjectDTO projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
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
    }
}
