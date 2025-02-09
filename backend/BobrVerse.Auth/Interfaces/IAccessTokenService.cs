using System.Security.Claims;

namespace BobrVerse.Auth.Interfaces
{
    public interface IAccessTokenService
    {
        string GenerateAccessToken(Dictionary<string, object> claims);
        bool TryValidateAccessToken(string token, out ClaimsPrincipal? claimsPrincipal);
    }
}
