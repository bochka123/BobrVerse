using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Entities.Quest.Tasks;

namespace BobrVerse.Dal.Entities.Quest
{
    public class QuizTaskStatus
    {
        public Guid Id { get; set; }
        public Guid QuizTaskId { get; set; }
        public QuizTask QuizTask { get; set; } = null!;

        public Guid QuestResponseId { get; set; }
        public QuestResponse QuestResponse { get; set; } = null!;

        public TaskTypeEnum TaskType { get; set; }
        public QuestTaskStatusEnum Status { get; set; } = QuestTaskStatusEnum.NotStarted;
        public int CurrentAttempt = 1;
        public int? EarnedXp { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
