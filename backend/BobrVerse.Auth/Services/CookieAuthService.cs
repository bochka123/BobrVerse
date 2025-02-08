using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.Settings;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BobrVerse.Auth.Services
{
    public class CookieAuthService : IAuthService
    {
        private readonly IAccessTokenService accessTokenService;
        private readonly IRefreshTokenService refreshTokenService;
        private readonly CookieSettings cookieSettings;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CookieAuthService(
            IAccessTokenService accessTokenService,
            IRefreshTokenService refreshTokenService,
            AuthSettings authSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            this.accessTokenService = accessTokenService;
            this.refreshTokenService = refreshTokenService;
            this.cookieSettings = authSettings.Cookie;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> ValidateRequestAsync(HttpContext context)
        {
            var accessToken = context.Request.Cookies[cookieSettings.AccessTokenName];
            var refreshToken = context.Request.Cookies[cookieSettings.RefreshTokenName];

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return false;
            }

            Guid userId;
            if (accessTokenService.TryValidateAccessToken(accessToken, out var claimsPrincipal))
            {
                var userIdClaim = claimsPrincipal?.FindFirst(JwtRegisteredClaimNames.NameId)?.Value;
                if (userIdClaim == null || !Guid.TryParse(userIdClaim, out userId))
                {
                    return false;
                }
            }

            var storedToken = await refreshTokenService.GetValidatedRefreshTokenAsync(refreshToken);
            if (storedToken == null)
            {
                return false;
            }
            userId = storedToken.UserId;

            SetRefreshToken(storedToken.Token);
            SetAccessToken(userId);

            return true;
        }

        public void Logout()
        {
            var context = httpContextAccessor.HttpContext;
            if (context == null)
            {
                return;
            }

            context.Response.Cookies.Delete(cookieSettings.RefreshTokenName);
            context.Response.Cookies.Delete(cookieSettings.AccessTokenName);
        }

        public void SetupAuth(Guid userId)
        {
            var refreshToken = refreshTokenService.GenerateRefreshToken(userId);
            SetRefreshToken(refreshToken.Token);
            SetAccessToken(userId);
        }

        private void SetAccessToken(Guid userId)
        {
            var context = httpContextAccessor.HttpContext;
            if (context == null)
            {
                return;
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, userId.ToString()),
            };
            var newAccessToken = accessTokenService.GenerateAccessToken(claims);
            SetCookie(context, cookieSettings.AccessTokenName, newAccessToken, DateTime.UtcNow.AddMinutes(cookieSettings.AccessTokenCookieMinutesExpire));
        }

        private void SetRefreshToken(string value)
        {
            var context = httpContextAccessor.HttpContext;
            if (context == null)
            {
                return;
            }

            SetCookie(context, cookieSettings.RefreshTokenName, value, DateTime.UtcNow.AddDays(cookieSettings.RefreshTokenCookieDaysExpire));
        }

        private void SetCookie(HttpContext context, string key, string value, DateTimeOffset timeOffset)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = timeOffset
            };

            context.Response.Cookies.Append(key, value, options);
        }
    }
}