using BobrVerse.Auth.Entities;
using BobrVerse.Dal.Entities.Quest;

namespace BobrVerse.Dal.Entities
{
    public class BobrProfile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ICollection<Quest.Quest> CreatedQuests { get; set; } = [];
        public ICollection<QuestResponse> QuestResponses { get; set; } = [];
        public ICollection<QuestRating> QuestRatings { get; set; } = [];
        public string Name { get; set; } = string.Empty;

        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        public BobrLevel Level { get; set; } = null!;
        public Guid LevelId {  get; set; }

        public int XP { get; set; }
        public int Logs { get; set; } = 100;
        public string? Url {  get; set; }
    }
}
