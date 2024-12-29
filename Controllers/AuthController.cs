using Microsoft.AspNetCore.Mvc;
using DigitalLendingPlatform.Contracts;
using Microsoft.AspNetCore.Identity.Data;
using DigitalLendingPlatform.DTOs;

namespace DigitalLendingPlatform.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto model)
        {
            try
            {
                var token = await _authService.RegisterAsync(model.FirstName, model.LastName, model.Email, model.Password);
                return Ok(new { Token = token });
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User already exists");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                var token = await _authService.LoginAsync(model.Email, model.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid email or password");
            }
        }
    }
}