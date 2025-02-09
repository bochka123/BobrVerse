using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.Redis;
using BobrVerse.Auth.Models.Settings;
using BobrVerse.Auth.Utilities;
using Newtonsoft.Json;

namespace BobrVerse.Auth.Services
{
    public class RefreshTokenService(AuthSettings authSettings, IRefreshTokenStore tokenStore) : IRefreshTokenService
    {
        private readonly RefreshTokenSettings refreshSettings = authSettings.RefreshToken;
        public RefreshTokenResponse GenerateRefreshToken(Guid userId, string ip)
        {
            var refreshToken = new RefreshTokenData
            {
                UserId = userId,
                Ip = ip,
                Expiration = DateTime.UtcNow.AddDays(refreshSettings.TokenLifeTimeInDays)
            };
            var id = Guid.NewGuid().ToString();
            var token = TokenEncryptionUtility.EncryptToken(id, refreshSettings.Secret);
            var encryptedData = TokenEncryptionUtility.EncryptToken(JsonConvert.SerializeObject(refreshToken), id);

            tokenStore.SaveRefreshTokenAsync(token, encryptedData);

            return new RefreshTokenResponse
            {
                UserId = userId,
                Value = token
            };
        }

        public async Task<RefreshTokenResponse?> GetValidatedRefreshTokenAsync(RefreshTokenValidateModel model)
        {
            var encryptedTokenData = await tokenStore.GetRefreshTokenAsync(model.Value);
            if (encryptedTokenData != null)
            {
                if (TokenEncryptionUtility.TryDecryptToken(model.Value, refreshSettings.Secret, out var id))
                {
                    if (TokenEncryptionUtility.TryDecryptToken(encryptedTokenData, id!, out var stringTokenData))
                    {
                        var tokenData = JsonConvert.DeserializeObject<RefreshTokenData>(stringTokenData!);
                        if (tokenData!.Ip == model.Ip && tokenData.Expiration > DateTime.UtcNow)
                        {
                            return GenerateRefreshToken(tokenData.UserId, model.Ip);
                        }
                    }
                }
            }

            return null;
        }
    }
}
