using BobrVerse.Common.Models.DTO.Quest.Task;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuizTaskService
    {
        Task<QuizTaskDTO> CreateAsync(CreateTaskDTO dto);
        Task<QuizTaskDTO> UpdateAsync(QuizTaskDTO dto);
        Task DeleteAsync(Guid Id);
        Task<QuizTaskDTO> GetByOrderAsync(Guid questId, int order);
    }
}
