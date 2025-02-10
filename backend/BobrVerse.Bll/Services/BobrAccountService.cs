using AutoMapper;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Common.Exceptions;
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

        public async Task EnsureProfileCreatedAsync()
        {
            var userId = userContextService.UserId;

            if (await context.BobrProfiles.AnyAsync(x => x.UserId == userId))
            {
                return;
            }

            var defaultLevel = await context.BobrLevels.AsNoTracking().OrderBy(x => x.Level).FirstOrDefaultAsync() 
                ?? throw new BobrException("No levels configured.");

            var newProfile = new BobrProfile
            {
                UserId = userId,
                Name = $"bobr {Guid.NewGuid().ToString()[..8]}",
                LevelId = defaultLevel.Id,
                XP = 0,
                Logs = 10
            };

            context.BobrProfiles.Add(newProfile);
            await context.SaveChangesAsync();
        }

        public async Task<MyBobrProfileDTO> UpdateProfileAsync(UpdateBobrProfileDTO dto)
        {
            var userId = userContextService.UserId;
            var profile = await context.BobrProfiles.Include(x => x.Level).FirstOrDefaultAsync(x => x.UserId == userId) 
                ?? throw new BobrException("Profile doesn't exists.");

            mapper.Map(dto, profile);
            await context.SaveChangesAsync();
            return mapper.Map<MyBobrProfileDTO>(profile);
        }
    }
}
