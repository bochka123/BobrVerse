using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Common.Models.DTO.File;
using Microsoft.AspNetCore.Http;

namespace BobrVerse.Bll.Interfaces
{
    public interface IBobrAccountService
    {
        Task<MyBobrProfileDTO?> GetMyProfileAsync();
        Task EnsureProfileCreatedAsync();
        Task<MyBobrProfileDTO> UpdateProfileAsync(UpdateBobrProfileDTO dto);
        Task<FileDto> UploadPhotoAsync(IFormCollection formCollection);
    }
}
