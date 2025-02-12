namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class QuestTaskResponseDTO
    {
        public bool Success { get; set; }
        public QuizTaskDTO? FirstTask { get; set; }
        public QuizTaskDTO? SecondTask { get; set; }
    }
}
