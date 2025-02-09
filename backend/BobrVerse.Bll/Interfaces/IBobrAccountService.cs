using BobrVerse.Common.Models.DTO.BobrProfile;

namespace BobrVerse.Bll.Interfaces
{
    public interface IBobrAccountService
    {
        Task<MyBobrProfileDTO?> GetMyProfileAsync();
        Task<MyBobrProfileDTO> CreateProfileAsync(CreateBobrProfileDTO dto);
    }
}
