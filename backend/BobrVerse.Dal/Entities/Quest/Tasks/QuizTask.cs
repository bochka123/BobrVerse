using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Interfaces.Tasks;

namespace BobrVerse.Dal.Entities.Quest.Tasks
{
    public class QuizTask : ICollectResourcesTask
    {
        public Guid Id { get; set; }
        public Guid QuestId {  get; set; }
        public Quest Quest { get; set; } = null!;
        public ICollection<QuizTaskStatus> TaskStatuses { get; set; } = [];
        public string? Url { get; set; }
        public TaskTypeEnum TaskType { get; set; }
        public string? ShortDescription { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsRequiredForNextStage { get; set; }
        public int? MaxAttempts {  get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public bool IsTemplate { get; set; } = false;

        public ICollection<Resource> RequiredResources { get; set; } = [];
        public string? CodeTemplate { get; set; }
        public int Order { get; set; }
    }
}
