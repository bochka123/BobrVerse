namespace BobrVerse.Common.Models.DTO.BobrLevel
{
    public class BobrLevelDTO
    {
        public int Level { get; set; }
        public int RequiredXP { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
