using BobrVerse.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IEmailPasswordAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public Task Register([FromBody] string email, string password) => authService.RegisterAsync(email, password);

        [HttpPost("login")]
        public Task Login([FromBody] string email, string password) => authService.LoginAsync(email, password);

        [HttpPost("logout")]
        public void Logout() => authService.Logout();
    }
}
