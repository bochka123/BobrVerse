using BobrVerse.Common.Models.DTO.Quest;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuestService
    {
        Task<QuestDTO> CreateAsync(CreateQuestDTO dto);
        Task<ICollection<QuestDTO>> GetMyQuests();
        Task<ICollection<QuestDTO>> GetActiveQuests();
    }
}
