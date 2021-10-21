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
    public class CourseController : ControllerBase
    {
        private readonly IProjectService _courseService;

        public CourseController(IProjectService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetAsync()
        {
            var result = await _courseService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddCourse(ProjectCreateDTO courseDto)
        {
            var result = await _courseService.CreateAsync(courseDto);
            return Created(string.Empty, result);
        }
    }
}
