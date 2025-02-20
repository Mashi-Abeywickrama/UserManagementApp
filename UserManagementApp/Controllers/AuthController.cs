using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.Controllers
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authService.AuthenticateAsync(model.Email, model.Password);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { Token = token });
        }
    }
}
