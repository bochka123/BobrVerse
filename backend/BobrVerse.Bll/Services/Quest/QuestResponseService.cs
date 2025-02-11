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

namespace BobrVerse.Bll.Services.Quest
{
    public class QuestResponseService(BobrVerseContext context, IUserContextService userContextService, IMapper mapper, IQuizTaskService quizTaskService) : IQuestResponseService
    {
        private async Task<BobrProfile> GetProfileAsync()
        {
            var userId = userContextService.UserId;
            return await context.BobrProfiles.AsNoTracking().FirstAsync(x => x.UserId == userId);
        }

        public async Task<QuestResponseDTO> CreateAsync(Guid questId)
        {
            var quest = await context.Quests.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == questId)
                ?? throw new BobrException($"Quest with id {questId} not found.");

            var profile = await GetProfileAsync();
            var questResponse = new QuestResponse
            {
                Id = Guid.NewGuid(),
                ProfileId = profile.Id,
                QuestId = questId,
                QuestTitle = quest.Title,
                QuestDescription = quest.Description,
                Status = QuestResponseStatusEnum.InProgress,
            };

            await context.QuestResponses.AddAsync(questResponse);
            await context.SaveChangesAsync();

            var questResponseDto = mapper.Map<QuestResponseDTO>(questResponse);

            questResponseDto.FirstTask = await quizTaskService.GetByOrderAsync(questId, 1);
            questResponseDto.SecondTask = await quizTaskService.GetByOrderAsync(questId, 2);

            return questResponseDto;
        }

        public async Task<ICollection<QuestResponseDTO>> GetUserQuestResponsesAsync(int start, int end)
        {
            var profile = await GetProfileAsync();

            var questResponses = await context.QuestResponses
                .Where(qr => qr.ProfileId == profile.Id)
                .OrderBy(qr => qr.CompletedAt)
                .Skip(start)
                .Take(end - start + 1)
                .ToListAsync();

            return mapper.Map<ICollection<QuestResponseDTO>>(questResponses);
        }
    }
}
