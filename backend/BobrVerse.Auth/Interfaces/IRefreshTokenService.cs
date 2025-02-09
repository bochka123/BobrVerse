using BobrVerse.Auth.Models.Redis;

namespace BobrVerse.Auth.Interfaces
{
    public interface IRefreshTokenService
    {
        RefreshTokenResponse GenerateRefreshToken(Guid userId, string ip);
        Task<RefreshTokenResponse?> GetValidatedRefreshTokenAsync(RefreshTokenValidateModel model);
    }
}
