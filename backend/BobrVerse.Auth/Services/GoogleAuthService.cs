using BobrVerse.Auth.Context;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.DTO;

namespace BobrVerse.Auth.Services
{
    public class GoogleAuthService(IAuthContext context, IAuthService authService) : IGoogleAuthService
    {
        public Task SignInAsync(GoogleSignModel model)
        {
            throw new NotImplementedException();
        }
    }
}
