using AutoMapper;
using BobrVerse.Auth.Interfaces;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.DTO.Quest;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities.Quest;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuestRatingService(BobrVerseContext context, IMapper mapper, IUserContextService userContextService) : IQuestRatingService
    {
        public async Task CreateAsync(CreateQuestRatingDTO dto)
        {
            var questRating = mapper.Map<QuestRating>(dto);

            var questResponse = await context.QuestResponses.FirstOrDefaultAsync(qr => qr.Id == dto.QuestResponseId)
                ?? throw new BobrException($"Quest is not found with id {dto.QuestResponseId}");

            var quest = await context.Quests.FirstOrDefaultAsync(q => q.Id == questResponse.QuestId)
                ?? throw new BobrException($"QuestResponse is not found with id {questResponse.QuestId}");

            var bobrProfile = await context.BobrProfiles.FirstOrDefaultAsync(q => q.Id == userContextService.UserId)
                ?? throw new BobrException($"BobrProfile is not found with id {userContextService.UserId}");

            questRating.QuestResponseId = questResponse.Id;
            questRating.QuestId = quest.Id;
            questRating.BobrProfileId = bobrProfile.Id;
            questRating.CreatedAt = DateTime.UtcNow;

            await context.QuestRatings.AddAsync(questRating);
            await context.SaveChangesAsync();
        }

        public async Task<double> GetQuestRatingAsync(Guid questId)
        {
            var quest = await context.Quests.FirstOrDefaultAsync(q => q.Id == questId)
                ?? throw new BobrException($"Quest is not found with id {questId}");

            var questRatings = context.QuestRatings.Where(qr => qr.QuestId == questId);

            return await questRatings.AnyAsync() ? await questRatings.AverageAsync(r => r.Rating) : 0;
        }

        public async Task<double> GetUserRatingAsync()
        {
            var bobrProfile = await context.BobrProfiles.FirstOrDefaultAsync(q => q.Id == userContextService.UserId)
                ?? throw new BobrException($"BobrProfile is not found with id {userContextService.UserId}");

            var questRatings = context.QuestRatings.Where(qr => qr.BobrProfileId == bobrProfile.Id);

            return await questRatings.AnyAsync() ? await questRatings.AverageAsync(r => r.Rating) : 0;
        }
    }
}