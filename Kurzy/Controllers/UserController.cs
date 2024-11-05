using Kurzy.Models;
using Kurzy.Services;
using Kurzy.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("Users")]
        public IActionResult GetUsers(UserRole role)
        {
            return Ok(_service.GetUsers(role));
        }

        [Authorize]
        [HttpGet("User")]
        public IActionResult GetUser(int id)
        {
            return Ok(_service.GetUser(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("User")]
        public IActionResult AddUser([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_service.AddUser(newUser));
        }

        [Authorize]
        [HttpPut("User")]
        public IActionResult PutUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_service.PutUser(user));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("User")]
        public IActionResult DeleteUser(int id)
        {
            return Ok(_service.DeleteUser(id));
        }
    }
}