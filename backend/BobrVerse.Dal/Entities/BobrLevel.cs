namespace BobrVerse.Dal.Entities
{
    public class BobrLevel
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public int RequiredXP { get; set; }
        public int LogsToAdd { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
