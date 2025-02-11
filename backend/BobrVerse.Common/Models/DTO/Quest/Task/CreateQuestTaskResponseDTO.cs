namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class CreateQuestTaskResponseDTO
    {
        public Guid QuestResponseId { get; set; }
        public Guid QuestTaskId { get; set; }

        public string? Text { get; set; }
        public List<ResourceDTO>? Resources { get; set; }
    }
}
