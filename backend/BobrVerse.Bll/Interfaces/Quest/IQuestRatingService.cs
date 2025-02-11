using BobrVerse.Common.Models.DTO.Quest;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuestRatingService
    {
        Task CreateAsync(CreateQuestRatingDTO dto);
        Task<double> GetUserRatingAsync();
        Task<double> GetQuestRatingAsync(Guid questId);
    }
}