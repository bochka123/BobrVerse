using BobrVerse.Auth.Context;
using BobrVerse.Auth.Entities;
using BobrVerse.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Auth.Services
{
    public class EmailPasswordAuthService(IAuthContext context, IAuthService authService) : IEmailPasswordAuthService
    {
        public async Task RegisterAsync(string email, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                throw new UnauthorizedAccessException("Email is already registered");
            }

            var hashedPassword = HashPassword(password);

            user = new User
            {
                Email = email,
                PasswordHash = hashedPassword
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            authService.SetupAuth(user.Id);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !VerifyPassword(user.PasswordHash, password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            authService.SetupAuth(user.Id);
        }

        public void Logout() => authService.Logout();

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        private bool VerifyPassword(string hashedPassword, string password) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
