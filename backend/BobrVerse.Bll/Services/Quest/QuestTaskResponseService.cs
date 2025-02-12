using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Bll.Interfaces.Quest.TaskValidator;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities.Quest;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuestTaskResponseService(BobrVerseContext context, IQuestTaskValidatorFactory validatorFactory, IQuizTaskService quizTaskService) : IQuestTaskResponseService
    {
        public async Task AnswerAsync(CreateQuestTaskResponseDTO dto)
        {

            var task = await context.QuizTasks.Include(x => x.Quest).Include(x => x.RequiredResources).FirstOrDefaultAsync(x => x.Id == dto.QuestTaskId)
                ?? throw new BobrException($"Quest with id {dto.QuestTaskId} not found.");

            var validator = validatorFactory.GetValidator(task.TaskType);
            var taskResponse = await context.QuizTaskStatuses.FirstOrDefaultAsync() ?? new QuizTaskStatus
            {
                QuizTaskId = task.Id,
                QuestResponseId = dto.QuestResponseId,
                CompletedAt = DateTime.UtcNow
            };

            if (!validator.Validate(dto, task))
            {
                if (task.MaxAttempts != null && dto.Attempt < task.MaxAttempts)
                {  
                    // todo: add logic for attempt + 1
                    // return cur task if cur attempt < max or nothing do
                }

                taskResponse.Status = Common.Models.Quiz.Enums.QuestTaskStatusEnum.Failed;

                await context.SaveChangesAsync();
                if (task.IsRequiredForNextStage)
                {
                    
                }
            }
            else
            {
                taskResponse.Status = Common.Models.Quiz.Enums.QuestTaskStatusEnum.Completed;
            }

            await context.SaveChangesAsync();
        }
    }
}
