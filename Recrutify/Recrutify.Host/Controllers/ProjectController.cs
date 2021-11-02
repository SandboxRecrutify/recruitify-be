using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // [Authorize(Policy = Constants.Constants.Policies.ProjectAdminPolicy)]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IPrimarySkillService _primarySkillService;

        public ProjectController(IProjectService projectService, IPrimarySkillService primarySkillService)
        {
            _projectService = projectService;
            _primarySkillService = primarySkillService;
        }

        // [AllowAnonymous]
        [HttpGet("primary_skills")]
        public async Task<ActionResult<List<PrimarySkillDTO>>> GetPrimarySkillAsync()
        {
            var result = await _primarySkillService.GetAllAsync();
            return Ok(result);
        }

        // [Authorize(Policy = "ProjectReadPolicy")]
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
        public async Task<ActionResult<ProjectDTO>> UpateProject(ProjectDTO projectDto)
        {
            var projectExists = await _projectService.ExistsAsync(projectDto.Id);
            if (!projectExists)
            {
                return NotFound();
            }

            var result = await _projectService.UpdateAsync(projectDto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var projectExists = await _projectService.ExistsAsync(id);
            if (!projectExists)
            {
                return NotFound();
            }

            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        // [Authorize(Policy = Constants.Constants.Policies.ProjectReadPolicy)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync(Guid id)
        {
            var project = await _projectService.GetAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
    }
}
