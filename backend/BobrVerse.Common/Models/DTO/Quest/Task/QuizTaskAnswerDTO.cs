namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class QuizTaskAnswerDTO
    {
        public Guid QuizTaskId { get; set; }
        public List<Guid>? SelectedOptionIds { get; set; }
        public string? TextAnswer { get; set; }
        public List<string>? UploadedFileUrls { get; set; }
        public List<ResourceDTO>? ProvidedResources { get; set; }
        public Dictionary<string, object>? ExtraData { get; set; }
    }
}
