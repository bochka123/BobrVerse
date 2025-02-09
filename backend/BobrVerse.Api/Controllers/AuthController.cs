using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.DTO;
using BobrVerse.Common.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IEmailPasswordAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] UserPasswordModel model)
        {
            await authService.RegisterAsync(model);
            return Ok(new ApiResponse(true));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] UserPasswordModel model)
        {
            await authService.LoginAsync(model);
            return Ok(new ApiResponse(true));
        }

        [HttpPost("logout")]
        public ActionResult<ApiResponse> Logout()
        {
            authService.Logout();
            return Ok(new ApiResponse(true));
        }
    }
}
