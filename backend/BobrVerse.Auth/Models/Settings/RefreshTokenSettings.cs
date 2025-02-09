namespace BobrVerse.Auth.Models.Settings
{
    public class RefreshTokenSettings
    {
        public int TokenLifeTimeInDays { get; set; } = 14;
        public string Secret { get; set; }
    }
}
