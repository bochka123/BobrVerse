using BobrVerse.Api.Hubs;
using BobrVerse.Bll.Interfaces.Hubs;
using BobrVerse.Common.Models.DTO.Quest;
using Microsoft.AspNetCore.SignalR;

namespace BobrVerse.Api.Services
{
    public class QuestsHubService(IHubContext<QuestsHub> hubContext) : IQuestsHubService
    {
        public async Task NotifyCreated(QuestDTO quest, string userClaim)
        {
            var connections = QuestsHub.GetConnections(userClaim);

            foreach (var connectionId in connections)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("questCreated", quest);
            }
        }
    }
}
