using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IEmailPasswordAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public Task Register([FromBody] UserPasswordModel model) => authService.RegisterAsync(model);

        [HttpPost("login")]
        public Task Login([FromBody] UserPasswordModel model) => authService.LoginAsync(model);

        [HttpPost("logout")]
        public void Logout() => authService.Logout();
    }
}
