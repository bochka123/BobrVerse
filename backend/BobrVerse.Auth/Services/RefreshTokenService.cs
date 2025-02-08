using BobrVerse.Auth.Entities;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.Settings;
using BobrVerse.Auth.Utilities;

namespace BobrVerse.Auth.Services
{
    public class RefreshTokenService(JwtSettings jwtSettings, IRefreshTokenStore tokenStore) : IRefreshTokenService
    {
        public const int RefreshTokenLifeTimeInDays = 14;
        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                Expiration = TimeSpan.FromDays(RefreshTokenLifeTimeInDays)
            };

            refreshToken.Token = TokenEncryptionUtility.EncryptToken(refreshToken.Token, jwtSettings.RefreshSecret);
            tokenStore.SaveRefreshTokenAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken?> GetValidatedRefreshTokenAsync(string refreshToken)
        {
            if (!TokenEncryptionUtility.TryDecryptToken(refreshToken, jwtSettings.RefreshSecret, out var decryptedToken))
            {
                return null;
            }

            var storedRefreshToken = await tokenStore.GetRefreshTokenAsync(decryptedToken!);
            if (storedRefreshToken != null && storedRefreshToken.Expiration > TimeSpan.Zero)
            {
                storedRefreshToken.Expiration = TimeSpan.FromDays(RefreshTokenLifeTimeInDays);
                await tokenStore.SaveRefreshTokenAsync(storedRefreshToken);
                return storedRefreshToken;
            }
            return null;
        }
    }
}
