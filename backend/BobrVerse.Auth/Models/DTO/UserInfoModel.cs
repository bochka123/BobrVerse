using Newtonsoft.Json;

namespace BobrVerse.Auth.Models.DTO
{
    public class UserInfoModel
    {
        public string Email { get; set; }
        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }
    }
}
