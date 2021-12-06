using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.Exceptions;
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

        [Authorize(Policy = Constants.Policies.AllAccessPolicy)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("primary_skills")]
        public async Task<ActionResult<List<PrimarySkillDTO>>> GeAllPrimarySkillsAsync()
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
        public async Task<ActionResult<ProjectDTO>> AddProject([FromBody] CreateProjectDTO projectDto)
        {
            var result = await _projectService.CreateAsync(projectDto);
            return Created(string.Empty, result);
        }

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
        [HttpPut]
        public async Task<ActionResult<ProjectDTO>> UpateProject([FromBody] UpdateProjectDTO projectDto, CancellationToken cancellationToken)
        {
            var projectExists = await _projectService.ExistsAsync(projectDto.Id, cancellationToken);
            if (!projectExists)
            {
                return NotFound();
            }

            var result = await _projectService.UpdateAsync(projectDto);
            return Ok(result);
        }

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var projectExists = await _projectService.ExistsAsync(id, cancellationToken);
            if (!projectExists)
            {
                return NotFound();
            }

            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var project = await _projectService.GetAsync(id);
                return Ok(project);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize(Policy = Constants.Policies.AdminPolicy)]
        [HttpGet("primary_skills_and_staff")]
        public async Task<ActionResult<PrimarySkillsAndStaffDTO>> PrimarySkillsAndStaff()
        {
            var result = await _projectService.GetPrimarySkillsAndStaff(new List<Role> { Role.Recruiter, Role.Mentor, Role.Manager, Role.Interviewer });
            return Ok(result);
        }

        [HttpGet("{id:guid}/primary_skills")]
        public async Task<ActionResult<ProjectPrimarySkillDTO>> GetPrimarySkillsAsync([FromRoute] Guid id)
        {
            var primarySkills = await _projectService.GetPrimarySkills(id);
            if (primarySkills == null)
            {
                return NotFound();
            }

            return Ok(primarySkills);
        }
    }
}
