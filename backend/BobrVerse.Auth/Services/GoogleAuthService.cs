using BobrVerse.Auth.Context;
using BobrVerse.Auth.Entities;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.DTO;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Auth.Services
{
    public class GoogleAuthService(IAuthContext context, IAuthService authService) : IGoogleAuthService
    {
        public async Task SignInAsync(GoogleSignModel model)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(model.Credential);
                if (!payload.EmailVerified)
                {
                    throw new InvalidOperationException("Email is not verified.");
                }
                var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == payload.Email);
                if (user == null)
                {
                    user = new User
                    {
                        Email = payload.Email
                    };

                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                }

                authService.SetupAuth(user.Id);
            }
            catch (InvalidJwtException)
            {
                throw new UnauthorizedAccessException("Invalid Google token.");
            }
        }
    }
}
