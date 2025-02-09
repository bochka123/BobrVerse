using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BobrVerse.Auth.Services
{
    public class AccessTokenService(AuthSettings authSettings) : IAccessTokenService
    {
        private readonly JwtSettings jwtSettings = authSettings.Jwt;
        public string GenerateAccessToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(EnsureKeyLength(Encoding.UTF8.GetBytes(jwtSettings.Secret)));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.TokenLifetimeMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool TryValidateAccessToken(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            claimsPrincipal = null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private byte[] EnsureKeyLength(byte[] keyBytes)
        {
            if (keyBytes.Length < 32)
            {
                Array.Resize(ref keyBytes, 32);
            }
            else if (keyBytes.Length > 32)
            {
                Array.Resize(ref keyBytes, 32);
            }
            return keyBytes;
        }
    }
}
