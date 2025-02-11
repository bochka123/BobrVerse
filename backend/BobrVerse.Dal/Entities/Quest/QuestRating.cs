namespace BobrVerse.Dal.Entities.Quest
{
    public class QuestRating
    {
        public Guid Id { get; set; }
        public Guid? QuestId { get; set; }
        public Quest? Quest { get; set; }
        public Guid? BobrProfileId { get; set; }
        public BobrProfile? BobrProfile { get; set; }
        public Guid? QuestResponseId { get; set; }
        public QuestResponse? QuestResponse { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}