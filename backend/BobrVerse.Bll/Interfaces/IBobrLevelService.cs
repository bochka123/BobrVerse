using BobrVerse.Common.Models.DTO.BobrLevel;

namespace BobrVerse.Bll.Interfaces
{
    public interface IBobrLevelService
    {
        Task<IEnumerable<BobrLevelDTO>> GetAll();
    }
}
