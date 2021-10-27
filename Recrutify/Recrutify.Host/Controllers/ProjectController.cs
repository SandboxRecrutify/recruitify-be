using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

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
            var project = await _projectService.ExistsAsync(courseDto.Id);
            if (!project)
            {
                return NotFound();
            }

            var result = await _projectService.UpdateAsync(courseDto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var project = await _projectService.ExistsAsync(id);
            if (!project)
            {
                return NotFound();
            }

            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync(Guid id)
        {
            var project = await _projectService.ExistsAsync(id);
            if (!project)
            {
                return NotFound();
            }

            var result = await _projectService.GetAsync(id);
            return Ok(result);
        }
    }
}
