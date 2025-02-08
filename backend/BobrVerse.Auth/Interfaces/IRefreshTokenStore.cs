using BobrVerse.Auth.Entities;

namespace BobrVerse.Auth.Interfaces
{
    public interface IRefreshTokenStore
    {
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
    }
}
