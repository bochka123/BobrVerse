using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.BobrProfile;
using Microsoft.AspNetCore.Mvc;

namespace BobrVerse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [MainAuth]
    public class BobrProfileController(IBobrAccountService bobrAccountService) : ControllerBase
    {
        [HttpGet("myprofile")]
        public async Task<ApiResponse<MyBobrProfileDTO>> GetMyProfile() => new ApiResponse<MyBobrProfileDTO>(await bobrAccountService.GetMyProfileAsync());

        [HttpPost("create")]
        public async Task<ApiResponse<MyBobrProfileDTO>> Create(CreateBobrProfileDTO dto) => new ApiResponse<MyBobrProfileDTO>(await bobrAccountService.CreateProfileAsync(dto));
    }
}
