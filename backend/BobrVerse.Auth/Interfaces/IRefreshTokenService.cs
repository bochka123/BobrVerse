using BobrVerse.Auth.Entities;

namespace BobrVerse.Auth.Interfaces
{
    public interface IRefreshTokenService
    {
        RefreshToken GenerateRefreshToken(Guid userId);
        Task<RefreshToken?> GetValidatedRefreshTokenAsync(string refreshToken);
    }
}
