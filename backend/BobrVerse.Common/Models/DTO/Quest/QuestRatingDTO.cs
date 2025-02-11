namespace BobrVerse.Common.Models.DTO.Quest
{
    public class QuestRatingDTO
    {
        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public Guid? QuestId { get; set; }
        public Guid? BobrProfileId { get; set; }
    }
}