using BobrVerse.Auth.Context;
using BobrVerse.Auth.Interfaces;

namespace BobrVerse.Auth.Services
{
    public class GoogleAuthService(IAuthContext context, IAuthService authService) : IGoogleAuthService
    {
    }
}
