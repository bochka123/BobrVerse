using BobrVerse.Common.Models.DTO.Quest.Task;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuestTaskResponseService
    {
        Task AnswerAsync(CreateQuestTaskResponseDTO dto);
    }
}
