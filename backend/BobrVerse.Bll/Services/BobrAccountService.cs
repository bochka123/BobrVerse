using AutoMapper;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Models.DTO.BobrProfile;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services
{
    public class BobrAccountService(BobrVerseContext context, IUserContextService userContextService, IMapper mapper) : IBobrAccountService
    {
        public IQueryable<BobrProfile> BobrProfilesQueryable =>
            context.BobrProfiles.Include(x => x.Level).AsNoTracking();

        public async Task<MyBobrProfileDTO?> GetMyProfileAsync()
        {
            var profile = await BobrProfilesQueryable.FirstOrDefaultAsync(x => x.UserId == userContextService.UserId);
            return profile == null ? null : mapper.Map<MyBobrProfileDTO>(profile);
        }

        public async Task<MyBobrProfileDTO> CreateProfileAsync(CreateBobrProfileDTO dto)
        {
            var userId = userContextService.UserId;

            if (await context.BobrProfiles.AnyAsync(x => x.UserId == userId))
            {
                throw new InvalidOperationException("Profile already exists.");
            }

            var defaultLevel = await context.BobrLevels.OrderBy(x => x.Level).FirstOrDefaultAsync() 
                ?? throw new InvalidOperationException("No levels configured.");

            var newProfile = new BobrProfile
            {
                UserId = userId,
                Name = dto.Name,
                LevelId = defaultLevel.Id,
                Level = defaultLevel,
                XP = 0,
                Logs = 10
            };

            context.BobrProfiles.Add(newProfile);
            await context.SaveChangesAsync();

            return mapper.Map<MyBobrProfileDTO>(newProfile);
        }
    }
}
