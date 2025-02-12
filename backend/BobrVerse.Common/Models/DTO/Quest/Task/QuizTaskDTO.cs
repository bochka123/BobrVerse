namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class QuizTaskDTO
    {
        public Guid Id { get; set; }
        public string TaskType { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? ShortDescription { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsRequiredForNextStage { get; set; }
        public int? MaxAttempts { get; set; }
        public int? TimeLimitInSeconds { get; set; }
        public bool IsTemplate { get; set; } = false;
        public int Order { get; set; }

        public List<ResourceDTO> RequiredResources { get; set; } = [];
        public int? MaxCollectCalls { get; set; }

        public int? ForestSize { get; set; }
        public int? TreesToCut { get; set; }
        public bool? CutLargest { get; set; }

        public Guid? NextTaskId { get; set; }
    }
}
