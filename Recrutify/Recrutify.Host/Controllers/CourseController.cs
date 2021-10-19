using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess;
using Recrutify.Host.Configuration.Profiles;
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
        public ActionResult<List<Course>> Index()
        {
            return Ok(_courseService.GetAllAsync());
        }

        [HttpPost]
        public IActionResult AddCourse(CourseDto courseDto)
        {
            _courseService.Creat(courseDto);
            return CreatedAtRoute("Get", new { id = courseDto.Id }, courseDto);
        }
    }
}
