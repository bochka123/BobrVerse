using AutoMapper;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.DTO.Quest;
using BobrVerse.Common.Models.Quest.Enums;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;
using Microsoft.EntityFrameworkCore;
using QuestDb = BobrVerse.Dal.Entities.Quest.Quest;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuestService(IUserContextService userContextService, BobrVerseContext context, IMapper mapper) : IQuestService
    {
        private const int XpForCompleteToLogsModifier = 5;
        private const int XpForSuccessCompleteToLogsModifier = 2;
        private async Task<BobrProfile> GetProfileAsync()
        {
            var userId = userContextService.UserId;
            return await context.BobrProfiles.AsNoTracking().FirstAsync(x => x.UserId == userId);
        }
        public async Task<QuestDTO> CreateAsync(CreateQuestDTO dto)
        {
            var profile = await GetProfileAsync();
            var newQuest = mapper.Map<QuestDb>(dto);
            newQuest.Id = Guid.NewGuid();
            newQuest.CreatedAt = DateTime.UtcNow;
            newQuest.UpdatedAt = DateTime.UtcNow;
            newQuest.Status = QuestStatusEnum.Draft;
            newQuest.AuthorId = profile.Id;

            await context.Quests.AddAsync(newQuest);
            await context.SaveChangesAsync();

            return mapper.Map<QuestDTO>(newQuest);
        }
        public async Task<QuestDTO> UpdateAsync(QuestDTO dto)
        {
            var profile = await GetProfileAsync();
            var quest = await context.Quests.FirstOrDefaultAsync(x => x.Id == dto.Id && x.AuthorId == profile.Id)
                ?? throw new BobrException("Quest not found.");

            mapper.Map(dto, quest);
            if (quest.Status == QuestStatusEnum.Active)
            {
                var cost = dto.XpForSuccess * XpForSuccessCompleteToLogsModifier + dto.XpForComplete * XpForCompleteToLogsModifier;
                if (profile.Logs < cost)
                {
                    throw new BobrException($"Your log balance is {profile.Logs}, but must be greater than {cost} to create quest.");
                }
            }

            await context.SaveChangesAsync();

            return mapper.Map<QuestDTO>(dto);
        }

        public async Task<ICollection<QuestDTO>> GetMyQuests()
        {
            var profile = await GetProfileAsync();
            var quests = await context.Quests.Where(x => x.AuthorId == profile.Id).AsNoTracking().ToListAsync();
            return mapper.Map<ICollection<QuestDb>, ICollection<QuestDTO>>(quests);
        }

        public async Task<ICollection<QuestDTO>> GetActiveQuests()
        {
            var profile = await GetProfileAsync();

            var quests = await context.Quests
                .Where(x =>
                x.AuthorId != profile.Id &&
                x.Status == QuestStatusEnum.Active)
                .ToListAsync();

            return mapper.Map<ICollection<QuestDb>, ICollection<QuestDTO>>(quests);
        }

    }
}
