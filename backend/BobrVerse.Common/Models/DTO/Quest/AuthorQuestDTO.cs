using BobrVerse.Common.Models.Quest.Enums;

namespace BobrVerse.Common.Models.DTO.Quest
{
    public class AuthorQuestDTO
    {
        public Guid Id { get; set; }
        public Guid? AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int XpForComplete { get; set; }
        public int XpForSuccess { get; set; }
        public QuestStatusEnum Status { get; set; }
        public int? TimeLimitInSeconds { get; set; }
        public string? Url { get; set; }
    }
}
