using BobrVerse.Common.Models.DTO.Quest.Task;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuizTaskService
    {
        Task CreateAsync(CreateTaskDTO dto);
    }
}
