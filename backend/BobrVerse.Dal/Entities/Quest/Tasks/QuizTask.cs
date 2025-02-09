using BobrVerse.Common.Models.Quiz.Enums;

namespace BobrVerse.Dal.Entities.Quest.Tasks
{
    public abstract class QuizTask
    {
        public Guid Id { get; set; }
        public Quest Quest { get; set; } = null!;
        public Guid QuestId { get; set; }
        public ICollection<QuizTaskStatus> TaskStatuses { get; set; } = [];
        public string? Url { get; set; }
        public TaskTypeEnum TaskType { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsRequiredForNextStage { get; set; }
        public int? MaxAttempts {  get; set; }
        public TimeSpan? TimeLimit { get; set; }
    }
}
