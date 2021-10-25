using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.Servises.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAsync()
        {
            var result = await _projectService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> AddProject(CreateProjectDTO projectDto)
        {
            var result = await _projectService.CreateAsync(projectDto);
            return Created(string.Empty, result);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDTO>> UpateProject(ProjectDTO courseDto)
        {
            var result = await _projectService.UpdateAsync(courseDto);
            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task RemoveIDAsync(Guid id)
        {
            await _projectService.RemoveIDAsync(id);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ProjectDTO>> GetIDAsync(Guid id)
        {
            var result = await _projectService.GetIDAsync(id);
            return Ok(result);
        }
    }
}
