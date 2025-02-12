using BobrVerse.Common.Models.DTO.Quest;

namespace BobrVerse.Bll.Interfaces.Hubs
{
    public interface IQuestsHubService
    {
        Task NotifyCreated(QuestDTO quest, string userClaim);
    }
}
