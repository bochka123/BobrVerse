using BobrVerse.Auth.Models.DTO;

namespace BobrVerse.Auth.Interfaces
{
    public interface IGoogleAuthService
    {
        public Task SignInAsync(GoogleSignModel model);
    }
}
