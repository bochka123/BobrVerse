using BobrVerse.Common.Models.DTO.File;
using BobrVerse.Common.Models.DTO.Quest.Task;
using Microsoft.AspNetCore.Http;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuizTaskService
    {
        Task<QuizTaskDTO> CreateAsync(CreateTaskDTO dto);
        Task<QuizTaskDTO> UpdateAsync(QuizTaskDTO dto);
        Task DeleteAsync(Guid Id);
        Task<QuizTaskDTO?> GetByOrderAsync(Guid questId, int order);
        Task<QuizTaskDTO> GetByIdAsync(Guid taskId);
        Task<FileDto> UploadPhotoAsync(IFormCollection formCollection, Guid questTaskId);
        Task<bool> DeletePhotoAsync(Guid questTaskId);
        ICollection<QuestTaskTypeInfoDTO> GetInfoForCreatingTasks();
    }
}
