namespace BobrVerse.Auth.Models.Settings
{
    public class AuthSettings
    {
        public RedisSettings Redis { get; set; }
        public CookieSettings Cookie { get; set; }
        public JwtSettings Jwt { get; set; }
        public RefreshTokenSettings RefreshToken { get; set; }
    }
}
