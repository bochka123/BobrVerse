using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.DTO;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.BobrProfile;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IEmailPasswordAuthService authService, IBobrAccountService accountService, IGoogleAuthService googleAuthService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] UserPasswordModel model)
        {
            await authService.RegisterAsync(model);
            await accountService.EnsureProfileCreatedAsync();
            return Ok(new ApiResponse<MyBobrProfileDTO>(true));
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

        [HttpPost("google")]
        public async Task<ActionResult<ApiResponse>> Google([FromBody] GoogleSignModel model)
        {
            await googleAuthService.SignInAsync(model);
            await accountService.EnsureProfileCreatedAsync();
            return Ok(new ApiResponse(true));
        }
    }
}
