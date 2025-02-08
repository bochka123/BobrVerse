namespace BobrVerse.Auth.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public TimeSpan Expiration { get; set; }
        public Guid UserId { get; set; }
    }
}
