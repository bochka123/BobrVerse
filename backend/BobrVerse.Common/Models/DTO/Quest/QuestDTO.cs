namespace BobrVerse.Common.Models.DTO.Quest
{
    public class QuestDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int XpForComplete { get; set; }
        public int XpForSuccess { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? TimeLimitInSeconds { get; set; }
        public string? Url { get; set; }
        public int NumberOfTasks { get; set; }
    }
}
