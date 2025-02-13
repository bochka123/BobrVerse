namespace BobrVerse.Common.Models.DTO.Quest
{
    public class QuestRatingDTO
    {
        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int VotesCount { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string? QuestTitle { get; set; }
        public Guid? BobrProfileId { get; set; }
    }
}