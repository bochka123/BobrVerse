using BobrVerse.Common.Models.DTO.Quest;

namespace BobrVerse.Bll.Interfaces.Quest
{
    public interface IQuestService
    {
        Task<AuthorQuestDTO> CreateAsync(CreateQuestDTO dto);
    }
}
