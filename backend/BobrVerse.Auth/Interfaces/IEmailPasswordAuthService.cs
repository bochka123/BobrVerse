using BobrVerse.Auth.Models.DTO;

namespace BobrVerse.Auth.Interfaces
{
    public interface IEmailPasswordAuthService
    {
        Task RegisterAsync(UserPasswordModel model);
        Task LoginAsync(UserPasswordModel model);
        void Logout();
    }
}
