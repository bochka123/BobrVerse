using BobrVerse.Common.Models.DTO.Quest.Task;

namespace BobrVerse.Common.Models.DTO.Quest
{
    public class ViewQuestDTO: QuestDTO
    {
        public QuizTaskDTO CurrentTask { get; set; } = null!;
        public QuizTaskDTO? NextQuestion { get; set; }
    }
}
