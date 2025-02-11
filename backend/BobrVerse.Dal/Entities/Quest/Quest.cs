using BobrVerse.Common.Models.Quest.Enums;
using BobrVerse.Dal.Entities.Quest.Tasks;

namespace BobrVerse.Dal.Entities.Quest
{
    public class Quest
    {
        public Guid Id { get; set; }
        public BobrProfile? Author { get; set; }
        public Guid? AuthorId {  get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<QuizTask> Tasks { get; set; } = [];
        public ICollection<QuestResponse> QuestResponses {  get; set; } = [];
        public ICollection<QuestRating> QuestRatings { get; set; } = [];
        public int XpForComplete { get; set; }
        public int XpForSuccess { get; set; }
        public QuestStatusEnum Status { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public string? Url { get; set; }
    }
}
