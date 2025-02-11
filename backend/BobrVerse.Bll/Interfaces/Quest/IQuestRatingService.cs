using BobrVerse.Common.Models.DTO.Quest;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuestRatingService
    {
        Task CreateAsync(CreateQuestRatingDTO dto);
        Task<double> GetAverageUserRatingAsync();
        Task<double> GetAverageQuestRatingAsync(Guid questId);
        Task<ICollection<QuestRatingDTO>> GetQuestRatingsAsync(Guid questId, int start, int end);
        Task<ICollection<QuestRatingDTO>> GetUserRatingsAsync(int start, int end);
    }
}