using BobrVerse.Common.Models.DTO.Quest;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuestRatingService
    {
        Task CreateAsync(CreateQuestRatingDTO dto);
    }
}