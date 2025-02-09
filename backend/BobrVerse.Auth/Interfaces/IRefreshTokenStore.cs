using BobrVerse.Auth.Models.Redis;

namespace BobrVerse.Auth.Interfaces
{
    public interface IRefreshTokenStore
    {
        Task SaveRefreshTokenAsync(string key, string value);
        Task<string?> GetRefreshTokenAsync(string key);
    }
}
