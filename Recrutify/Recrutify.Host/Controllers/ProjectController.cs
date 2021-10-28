using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
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

        [EnableCors("GetPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAsync()
        {
            var result = await _projectService.GetAllAsync();
            return Ok(result);
        }

        [EnableCors("PostPolice")]
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> AddProject(CreateProjectDTO projectDto)
        {
            var result = await _projectService.CreateAsync(projectDto);
            return Created(string.Empty, result);
        }

        [EnableCors("PutPolice")]
        [HttpPut]
        public async Task<ActionResult<ProjectDTO>> UpateProject(ProjectDTO courseDto)
        {
            var project = await _projectService.GetAsync(courseDto.Id);
            if (project == null)
            {
                return NotFound();
            }

            var result = await _projectService.UpdateAsync(courseDto);
            return Ok(result);
        }

        [EnableCors("GetPolice")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var project = await _projectService.GetAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        [EnableCors("GetPolice")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync(Guid id)
        {
            var project = await _projectService.GetAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var result = await _projectService.GetAsync(id);
            return Ok(result);
        }
    }
}
