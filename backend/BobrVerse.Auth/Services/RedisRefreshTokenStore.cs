using BobrVerse.Auth.Entities;
using BobrVerse.Auth.Interfaces;
using StackExchange.Redis;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace BobrVerse.Auth.Services
{
    public class RedisRefreshTokenStore : IRefreshTokenStore
    {
        private readonly IDatabase _database;

        public RedisRefreshTokenStore(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            var key = GetKey(refreshToken.Token);
            var value = JsonSerializer.Serialize(refreshToken);
            await _database.StringSetAsync(key, value, refreshToken.Expiration);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            var key = GetKey(token);
            var value = await _database.StringGetAsync(key);

            return value.HasValue ? JsonSerializer.Deserialize<RefreshToken>(value!) : null;
        }

        private static string GetKey(string token) => $"refresh_token:{token}";
    }
}
