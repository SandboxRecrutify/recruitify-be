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

    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IPrimarySkillService _primarySkillService;
        private readonly IUserService _userService;

        public ProjectsController(IProjectService projectService, IPrimarySkillService primarySkillService, IUserService userService)
        {
            _projectService = projectService;
            _primarySkillService = primarySkillService;
            _userService = userService;
        }

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
        [HttpGet("primary_skills")]
        public async Task<ActionResult<List<PrimarySkillDTO>>> GetPrimarySkillAsync()
        {
            var result = await _primarySkillService.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAsync()
        {
            var result = await _projectService.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> AddProject(CreateProjectDTO projectDto)
        {
            var result = await _projectService.CreateAsync(projectDto);
            return Created(string.Empty, result);
        }

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
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

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
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

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
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

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [HttpGet("primary_skills_and_staff")]
        public async Task<ActionResult<PrimarySkillsAndStaffDTO>> PrimarySkillsAndStaff()
        {
            var result = new PrimarySkillsAndStaffDTO()
            {
                PrimarySkills = await _primarySkillService.GetAllAsync(),
                StaffGroup = await _userService.GetByGroupRoleAsync(),
            };

            return Ok(result);
        }
    }
}
