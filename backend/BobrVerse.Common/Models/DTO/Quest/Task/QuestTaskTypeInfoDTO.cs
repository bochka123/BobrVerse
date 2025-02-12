namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class QuestTaskTypeInfoDTO
    {
        public string Name { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
        public IDictionary<string, string>? Parameters { get; set; }
        public IEnumerable<string> Keywords { get; set; }
    }
}
