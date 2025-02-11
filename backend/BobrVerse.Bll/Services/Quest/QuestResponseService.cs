using BobrVerse.Auth.Interfaces;
using BobrVerse.Auth.Services;
using BobrVerse.Common.Exceptions;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuestResponseService(BobrVerseContext context, IUserContextService userContextService)
    {
        private async Task<BobrProfile> GetProfileAsync()
        {
            var userId = userContextService.UserId;
            return await context.BobrProfiles.AsNoTracking().FirstAsync(x => x.UserId == userId);
        }
        public async Task CreateAsync(Guid questId)
        {
            var quest = await context.Quests.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == questId)
                ?? throw new BobrException($"Quest with id {questId} not found.");

            var profile = await GetProfileAsync();
            var questResponse = new QuestResponse
            {
                ProfileId = profile.Id,
                QuestId = questId,
                QuestTitle = quest.Title,
                QuestDescription = quest.Description,
            };

        }
    }
}
