namespace BobrVerse.Dal.Entities.Quest
{
    public class QuestResponse
    {
        public Guid Id { get; set; }
        public Quest Quest { get; set; } = null!;
        public Guid QuestId { get; set; }
        public BobrProfile Profile { get; set; } = null!;
        public Guid ProfileId { get; set; }
        public string QuestTitle { get; set; } = string.Empty;
        public string QuestDescription { get; set; } = string.Empty;
        public int XpEarned { get; set; } = 0;
        public bool IsCompleted { get; set; } = false;
        public ICollection<QuizTaskStatus> TaskStatuses { get; set; } = [];
        public int TotalXp { get; set; } = 0;
        public DateTime? CompletedAt { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public Guid? QuestRatingId { get; set; }
        public QuestRating? QuestRating { get; set; }
    }
}
