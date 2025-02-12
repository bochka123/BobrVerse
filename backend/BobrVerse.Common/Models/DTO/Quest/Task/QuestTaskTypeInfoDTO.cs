namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class QuestTaskTypeInfoDTO
    {
        public string TaskType { get; set; }
        public string Description { get; set; }
        public IEnumerable<string>? Parameters { get; set; }
    }
}
