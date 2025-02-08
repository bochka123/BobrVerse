using Microsoft.AspNetCore.Http;

namespace BobrVerse.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateRequestAsync(HttpContext context);
        void Logout();
        void SetupAuth(Guid userId);
    }
}
