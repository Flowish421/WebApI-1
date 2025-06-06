using Microsoft.AspNetCore.Mvc;
using WebApI_1.DTOs;
using WebApI_1.Services;

namespace WebApI_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (result == "Användare skapad!")
                return Ok(new { message = result });

            return BadRequest(new { error = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token.StartsWith("Ogiltiga"))
                return Unauthorized(new { error = token });

            return Ok(new { token });
        }
    }
}
