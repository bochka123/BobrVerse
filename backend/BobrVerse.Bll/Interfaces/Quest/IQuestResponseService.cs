using BobrVerse.Common.Models.DTO.Quest;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuestResponseService
    {
        Task<QuestResponseDTO> CreateAsync(Guid questId);
        Task<ICollection<QuestResponseDTO>> GetUserQuestResponsesAsync(int start, int end);
    }
}