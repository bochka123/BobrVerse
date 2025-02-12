using BobrVerse.Common.Models.Quiz.Enums;

namespace BobrVerse.Dal.Entities.Quest.Tasks
{
    public class QuizTask
    {
        public Guid Id { get; set; }
        public Guid QuestId { get; set; }
        public Quest Quest { get; set; } = null!;
        public ICollection<QuizTaskStatus> TaskStatuses { get; set; } = [];
        public string? Url { get; set; }
        public TaskTypeEnum TaskType { get; set; }
        public string? ShortDescription { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsRequiredForNextStage { get; set; }
        public int? MaxAttempts { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public bool IsTemplate { get; set; } = false;
        public int Order { get; set; }

        // Collect Resources
        public int? MaxCollectCalls { get; set; }
        public ICollection<Resource> RequiredResources { get; set; } = [];

        // Cut Trees In Forest
        public int? ForestSize { get; set; }
        public int? TreesToCut { get; set; }
        public bool? CutLargest { get; set; }
    }
}
