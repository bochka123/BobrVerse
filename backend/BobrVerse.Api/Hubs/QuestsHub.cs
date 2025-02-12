using BobrVerse.Api.Services;
using BobrVerse.Auth.Attributes;
using BobrVerse.Auth.Entities;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Models.Settings;
using BobrVerse.Common.Models.DTO.Quest;
using Microsoft.AspNetCore.SignalR;

namespace BobrVerse.Api.Hubs
{
    [MainAuth]
    public class QuestsHub(AuthSettings authSettings, IUserContextService userContextService, IAccessTokenService accessTokenService) : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new();
        private readonly CookieSettings cookieSettings = authSettings.Cookie;

        public static IEnumerable<string> GetConnections(string userClaim)
        {
            return _connections.GetConnections(userClaim);
        }

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                var accessToken = httpContext.Request.Cookies[cookieSettings.AccessTokenName];

                if (accessTokenService.TryValidateAccessToken(accessToken, out var claimsPrincipal))
                {
                    var userId = claimsPrincipal?.FindFirst(nameof(User))?.Value;
                    _connections.Add(userId, Context.ConnectionId);
                }
            }

            return base.OnConnectedAsync();
        }

        public async Task NotifyCreated(QuestDTO quest, string userClaim)
        {
            if (_connections.GetConnections(userClaim) is IEnumerable<string> connectionIds)
            {
                foreach (var connectionId in connectionIds)
                {
                    await Clients.Client(connectionId).SendAsync("questCreated", quest);
                }
            }
        }
    }
}
