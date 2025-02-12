namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class QuestTaskResponseDTO
    {
        public int? XpGained { get; set; }
        public bool IsFinished { get; set; } = false;
        public bool Success { get; set; }
        public QuizTaskDTO? CurrentTask { get; set; }
        public QuizTaskDTO? NextTask { get; set; }
    }
}
