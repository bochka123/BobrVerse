namespace BobrVerse.Auth.Models.Settings
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; }
        public int DaysToExpire { get; set; } = 7;
    }
}
