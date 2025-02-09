using AutoMapper;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Models.DTO.BobrLevel;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services
{
    public class BobrLevelService(BobrVerseContext context, IMapper mapper): IBobrLevelService
    {
        public async Task<IEnumerable<BobrLevelDTO>> GetAll() 
            => mapper.Map<IEnumerable<BobrLevel>, IEnumerable<BobrLevelDTO>>(await context.BobrLevels.AsNoTracking().OrderBy(x => x.Level).ToListAsync());
    }
}
