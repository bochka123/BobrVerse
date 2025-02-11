namespace BobrVerse.Common.Models.DTO.Quest
{
    public class CreateQuestRatingDTO
    {
        public Guid QuestResponseId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}