using BobrVerse.Auth.Attributes;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Models.Api;
using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Common.Models.DTO.File;
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

        [HttpPut("update")]
        public async Task<ApiResponse<MyBobrProfileDTO>> Update(UpdateBobrProfileDTO dto) => new ApiResponse<MyBobrProfileDTO>(await bobrAccountService.UpdateProfileAsync(dto));

        [HttpPut("uploadPhoto")]
        public async Task<ApiResponse<FileDto>> UploadPhoto() => new ApiResponse<FileDto>(await bobrAccountService.UploadPhotoAsync(await Request.ReadFormAsync()));

        [HttpDelete("deletePhoto")]
        public async Task<ApiResponse> DeletePhoto() => new ApiResponse(await bobrAccountService.DeletePhotoAsync());
    }
}
