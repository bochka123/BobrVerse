using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Dal.Entities.Quest.Tasks;

namespace BobrVerse.Bll.Interfaces.Quest.TaskValidator
{
    public interface IQuestTaskValidator
    {
        public TaskValidationState Validate(CreateQuestTaskResponseDTO dto, QuizTask task);
    }
}
