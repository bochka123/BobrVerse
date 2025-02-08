namespace BobrVerse.Auth.Models.Settings
{
    public class CookieSettings
    {
        public string AccessTokenName { get; set; } = "accessToken";
        public string RefreshTokenName { get; set; } = "refreshToken";
        public int AccessTokenCookieMinutesExpire { get; set; } = 15;
        public int RefreshTokenCookieDaysExpire { get; set; } = 7;
    }
}
