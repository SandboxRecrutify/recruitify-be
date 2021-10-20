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
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
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
        public async Task<ActionResult> AddCourse(CreateCourseDto courseDto)
        {
            var result = await _courseService.CreateAsync(courseDto);
            return Created(string.Empty, result);
        }
    }
}
