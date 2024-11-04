using Kurzy.Models;
using Kurzy.Services;
using Kurzy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kurzy.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _service;
        public CourseController(ILogger<CourseController> logger, ICourseService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("Courses")]
        public IActionResult GetCourses()
        {
            return Ok(_service.GetCourses());
        }

        [HttpGet("Course")]
        public IActionResult GetCourse(int id)
        {
            return Ok(_service.GetCourse(id));
        }

        [HttpPost("Course")]
        public IActionResult AddCourse([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_service.AddCourse(course));
        }

        [HttpPut("Course")]
        public IActionResult PutCourse([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_service.PutCourse(course));  
        }

        [HttpDelete("Course")]
        public IActionResult DeleteCourse(int id)
        {
            return Ok(_service.DeleteCourse(id));
        }
    }
}
