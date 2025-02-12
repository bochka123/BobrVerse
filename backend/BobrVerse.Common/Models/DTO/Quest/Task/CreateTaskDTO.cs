namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class CreateTaskDTO
    {
        public Guid QuestId { get; set; }
        public string TaskType { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsRequiredForNextStage { get; set; }
        public int? MaxAttempts { get; set; }
        public int? TimeLimitInSeconds { get; set; }
        public bool IsTemplate { get; set; } = false;
        public int Order { get; set; }

        public List<ResourceDTO> RequiredResources { get; set; } = [];
        public int? MaxCollectCalls { get; set; }
    }
}
