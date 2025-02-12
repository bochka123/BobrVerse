namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class CreateQuestTaskResponseDTO
    {
        public Guid QuestResponseId { get; set; }
        public Guid QuestTaskId { get; set; }
        public int Attempt = 1;
        public string? Text { get; set; }
    }
}
