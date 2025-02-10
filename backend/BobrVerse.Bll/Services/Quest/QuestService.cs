using AutoMapper;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.DTO.Quest;
using BobrVerse.Common.Models.Quest.Enums;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using QuestDb = BobrVerse.Dal.Entities.Quest.Quest;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuestService(IUserContextService userContextService, BobrVerseContext context, IMapper mapper): IQuestService
    {
        private const int XpForCompleteToLogsModifier = 5;
        private const int XpForSuccessCompleteToLogsModifier = 2;
        private async Task<BobrProfile> GetProfileAsync()
        {
            var userId = userContextService.UserId;
            return await context.BobrProfiles.AsNoTracking().FirstAsync(x => x.UserId == userId);
        }
        public async Task<AuthorQuestDTO> CreateAsync(CreateQuestDTO dto)
        {
            var profile = await GetProfileAsync();
            var cost = dto.XpForSuccess * XpForSuccessCompleteToLogsModifier + dto.XpForComplete * XpForCompleteToLogsModifier;
            if (profile.Logs < cost)
            {
                throw new BobrException($"Your log balance is {profile.Logs}, but must be greater than {cost} to create quest.");
            }
            profile.Logs -= cost;
            var newQuest = mapper.Map<QuestDb>(dto);
            newQuest.Id = Guid.NewGuid();
            newQuest.CreatedAt = DateTime.UtcNow;
            newQuest.UpdatedAt = DateTime.UtcNow;
            newQuest.Status = QuestStatusEnum.Draft;
            newQuest.AuthorId = profile.Id;

            await context.Quests.AddAsync(newQuest);
            await context.SaveChangesAsync();
            return mapper.Map<AuthorQuestDTO>(newQuest);
        }

        public async Task<ICollection<AuthorQuestDTO>> GetMyQuests()
        {
            var profile = await GetProfileAsync();
            var quests = await context.Quests.Where(x => x.AuthorId == profile.Id).AsNoTracking().ToListAsync();
            return mapper.Map<ICollection<QuestDb>, ICollection<AuthorQuestDTO>>(quests);
        }
    }
}
