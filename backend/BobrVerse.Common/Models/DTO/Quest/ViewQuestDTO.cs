namespace BobrVerse.Common.Models.DTO.Quest
{
    public class ViewQuestDTO: AuthorQuestDTO
    {
        public ICollection<Guid> Tasks { get; set; } = [];
    }
}
