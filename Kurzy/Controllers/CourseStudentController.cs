using Kurzy.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kurzy.Controllers
{
    [ApiController]
    public class CourseStudentController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseStudentService _service;
        public CourseStudentController(ILogger<CourseController> logger, ICourseStudentService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize]
        [HttpPost("studentToCourse")]
        public IActionResult AddStudentToCourse(int idCourse, int idStudent)
        {
            return Ok(_service.AddStudentToCourse(idCourse, idStudent));
        }

        [Authorize]
        [HttpDelete("studentFromCourse")]
        public IActionResult DeleteStudentFromCourse(int idCourse, int idStudent)
        {
            return Ok(_service.DeleteStudentFromCourse(idCourse, idStudent));
        }
    }
}
