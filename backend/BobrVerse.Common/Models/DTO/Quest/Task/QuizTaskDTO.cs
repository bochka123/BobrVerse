namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class QuizTaskDTO
    {
        public Guid Id { get; set; }
        public string TaskType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRequiredForNextStage { get; set; }
        public int? MaxAttempts { get; set; }
        public int? TimeLimitInSeconds { get; set; }

        public List<ResourceDTO> RequiredResources { get; set; } = [];
        public string? CodeTemplate { get; set; }
    }
}
