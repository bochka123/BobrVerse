using AutoMapper;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Extenions;
using BobrVerse.Common.Models.DTO.Quest;
using BobrVerse.Common.Models.Quest.Enums;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
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

            var quests = await context.Quests
                .Where(x => x.AuthorId == profile.Id)
                .AsNoTracking()
                .ToListAsync();

            var questIds = quests.Select(q => q.Id).ToList();
            var tasksCounts = await context.QuizTasks
                .Where(t => questIds.Contains(t.QuestId))
                .GroupBy(t => t.QuestId)
                .Select(g => new { QuestId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.QuestId, x => x.Count);

            var questDtos = mapper.Map<ICollection<QuestDb>, ICollection<QuestDTO>>(quests);
            foreach (var questDto in questDtos)
            {
                questDto.NumberOfTasks = tasksCounts.TryGetValue(questDto.Id, out var count) ? count : 0;
            }

            return questDtos;
        }

        public async Task<ICollection<ViewQuestDTO>> GetActiveQuests()
        {
            var profile = await GetProfileAsync();

            var quests = await context.Quests
                .Where(x =>
                x.AuthorId != profile.Id &&
                x.Status == QuestStatusEnum.Active)
                .Include(x => x.QuestResponses.Where(x => x.ProfileId == profile.Id))
                .ToListAsync();

            var questsDto = quests.Select(x =>
            {
                var quest = mapper.Map<ViewQuestDTO>(x);
                quest.UserStatus = x.QuestResponses.Any() ?
                    x.QuestResponses.OrderBy(x => x.Status).First().Status.GetDescription() :
                    QuestResponseStatusEnum.NotStarted.GetDescription();
                return quest;
            }).ToList();

            return mapper.Map<ICollection<QuestDb>, ICollection<ViewQuestDTO>>(quests);
        }

        public async Task<QuestDTO> GetQuestByIdAsync(Guid questId)
        {
            var quest = await context.Quests
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == questId) ?? throw new BobrException("Quest not found.");

            var tasksCount = await context.QuizTasks
                .AsNoTracking()
                .CountAsync(x => x.QuestId == questId);

            var questDto = mapper.Map<QuestDb, QuestDTO>(quest);

            questDto.NumberOfTasks = tasksCount;
            return questDto;
        }
    }
}
