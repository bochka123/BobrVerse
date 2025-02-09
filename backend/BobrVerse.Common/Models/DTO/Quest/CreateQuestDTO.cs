namespace BobrVerse.Common.Models.DTO.Quest
{
    public class CreateQuestDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int XpForComplete { get; set; }
        public int XpForSuccess { get; set; }
        public int? TimeLimitInSeconds { get; set; }
    }
}
