using BobrVerse.Common.Models.DTO.BobrLevel;

namespace BobrVerse.Common.Models.DTO.BobrProfile
{
    public class MyBobrProfileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public BobrLevelDTO Level { get; set; } = null!;
        public int XP { get; set; }
        public int Logs { get; set; }
        public string? Url { get; set; }
    }
}
