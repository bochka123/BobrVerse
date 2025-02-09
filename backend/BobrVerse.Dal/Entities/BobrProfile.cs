using BobrVerse.Auth.Entities;

namespace BobrVerse.Dal.Entities
{
    public class BobrProfile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        public BobrLevel Level { get; set; } = null!;
        public Guid LevelId {  get; set; }

        public int XP { get; set; }
        public int Logs { get; set; } = 10;
    }
}
