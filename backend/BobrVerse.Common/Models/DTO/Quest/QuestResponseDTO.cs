using BobrVerse.Common.Models.DTO.Quest.Task;

namespace BobrVerse.Common.Models.DTO.Quest
{
    public class QuestResponseDTO
    {
        public Guid Id { get; set; }
        public Guid QuestId { get; set; }
        public string QuestTitle { get; set; } = string.Empty;
        public string QuestDescription { get; set; } = string.Empty;
        public DateTime? StartedAt { get; set; }
        public int XpEarned { get; set; } = 0;
        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
        public QuizTaskDTO? CurrentTask { get; set; }
        public QuizTaskDTO? NextTask { get; set; }
    }
}