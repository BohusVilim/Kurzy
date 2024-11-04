using Kurzy.Models;
using Kurzy.Services;
using Kurzy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Kurzy.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("Users")]
        public IActionResult GetUsers(UserRole role)
        {
            return Ok(_service.GetUsers(role));
        }

        [HttpGet("User")]
        public IActionResult GetUser(int id)
        {
            return Ok(_service.GetUser(id));
        }

        [HttpPost("User")]
        public IActionResult AddUser([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_service.AddUser(newUser));
        }

        [HttpPut("User")]
        public IActionResult PutUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_service.PutUser(user));
        }

        [HttpDelete("User")]
        public IActionResult DeleteUser(int id)
        {
            return Ok(_service.DeleteUser(id));
        }
    }
}