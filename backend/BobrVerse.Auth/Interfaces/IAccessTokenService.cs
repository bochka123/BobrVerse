using BobrVerse.Auth.Entities;
using System.Security.Claims;

namespace BobrVerse.Auth.Interfaces
{
    public interface IAccessTokenService
    {
        string GenerateAccessToken(Claim[] claims);
        bool TryValidateAccessToken(string token, out ClaimsPrincipal? claimsPrincipal);
    }
}
