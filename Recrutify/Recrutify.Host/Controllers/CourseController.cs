using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Repositories;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseRepository _courseRepository;

        public CourseController(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public ActionResult<List<Course>> Index()
        {
            return Ok(_courseRepository.GetAllAsync());
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            _courseRepository.CreateAsync(course);
            return CreatedAtRoute("GetStudent", new { id = course.Id }, course);
        }
    }
}
