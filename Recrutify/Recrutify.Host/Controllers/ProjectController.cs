using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess;
using Recrutify.Services.Dtos;
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
        public async Task<ActionResult<ProjectDTO>> AddCourse(ProjectCreateDTO courseDto)
        {
            var result = await _projectService.CreateAsync(courseDto);
            return Created(string.Empty, result);
        }
    }
}
