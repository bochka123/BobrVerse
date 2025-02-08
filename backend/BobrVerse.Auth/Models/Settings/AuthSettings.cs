namespace BobrVerse.Auth.Models.Settings
{
    public class AuthSettings
    {
        public string RedisConnectionString {  get; set; }
        public string RefreshTokenLifeTimeInDays {  get; set; }
        public CookieSettings Cookie { get; set; }
        public JwtSettings Jwt { get; set; }
    }
}
