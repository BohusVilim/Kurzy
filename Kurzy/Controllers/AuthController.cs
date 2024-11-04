using Kurzy.Models;
using Kurzy.Services.Interfaces;
using Kurzy.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Kurzy.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(IOptions<JwtSettings> jwtSettings, ILogger<AuthController> logger, IAuthService authService)
        {
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_authService.IsAuthenticated(loginRequest)) 
            {
                var token = _authService.GenerateJwt(loginRequest.UserName);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

    }
}
