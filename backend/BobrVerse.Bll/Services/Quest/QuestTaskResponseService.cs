using AutoMapper;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Bll.Interfaces.Quest.TaskValidator;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Common.Models.Quest.Enums;
using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities.Quest;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuestTaskResponseService(
        BobrVerseContext context,
        IQuestTaskValidatorFactory validatorFactory,
        IQuizTaskService quizTaskService,
        IBobrAccountService accountService,
        IMapper mapper) : IQuestTaskResponseService
    {
        public async Task<QuestTaskResponseDTO> AnswerAsync(CreateQuestTaskResponseDTO dto)
        {
            var task = await context.QuizTasks
                .Include(x => x.Quest)
                .Include(x => x.RequiredResources)
                .FirstOrDefaultAsync(x => x.Id == dto.QuestTaskId)
                ?? throw new BobrException($"Task with id {dto.QuestTaskId} not found.");

            var validator = validatorFactory.GetValidator(task.TaskType);

            var existingTaskStatus = await context.QuizTaskStatuses
                .FirstOrDefaultAsync(x => x.QuizTaskId == task.Id && x.QuestResponseId == dto.QuestResponseId)
                ?? throw new BobrException($"Task response not found.");

            if (task.TimeLimit.HasValue && dto.SpentTime > task.TimeLimit.Value.TotalSeconds)
            {
                existingTaskStatus.EarnedXp = 0;
                existingTaskStatus.CompletedAt = DateTime.UtcNow;
                existingTaskStatus.Status = QuestTaskStatusEnum.Failed;

                await context.SaveChangesAsync();

                return new QuestTaskResponseDTO
                {
                    Success = false,
                    IsFinished = true,
                    CurrentTask = mapper.Map<QuizTaskDTO>(task)
                };
            }

            if (existingTaskStatus != null)
            {
                if (task.MaxAttempts != null && existingTaskStatus.CurrentAttempt >= task.MaxAttempts)
                {
                    throw new BobrException("Maximum attempts reached.");
                }

                existingTaskStatus.CurrentAttempt += 1;
                await context.SaveChangesAsync();

                return new QuestTaskResponseDTO
                {
                    Success = false,
                    IsFinished = false,
                    CurrentTask = mapper.Map<QuizTaskDTO>(task)
                };
            }

            var taskResponse = new QuizTaskStatus
            {
                QuizTaskId = task.Id,
                QuestResponseId = dto.QuestResponseId,
                CompletedAt = DateTime.UtcNow,
                CurrentAttempt = 1
            };


            var status = validator.Validate(dto, task);
            var response = new QuestTaskResponseDTO
            {
                Success = status.Success,
                ErrorMessage = status.ErrorMessage,
            };

            if (!status.Success)
            {
                if (task.IsRequiredForNextStage)
                {
                    var questResponse = await context.QuestResponses
                        .FirstAsync(x => x.Id == dto.QuestResponseId);

                    questResponse.Status = QuestResponseStatusEnum.Completed;
                    await HandleFinishQuest(dto.QuestResponseId, task.Quest,response);
                }
            }

            if (status.Success)
            {
                taskResponse.Status = QuestTaskStatusEnum.Completed;
            }
            else
            {
                taskResponse.Status = QuestTaskStatusEnum.Failed;
            }
            await context.QuizTaskStatuses.AddAsync(taskResponse);

            var nextTask = await quizTaskService.GetByOrderAsync(task.QuestId, task.Order + 1);
            if (nextTask != null)
            {
                response.CurrentTask = nextTask;
                response.NextTask = await quizTaskService.GetByOrderAsync(task.QuestId, task.Order + 2);
                return response;
            }

            return await HandleFinishQuest(dto.QuestResponseId, task.Quest, response);
        }

        private async Task<QuestTaskResponseDTO> HandleFinishQuest(Guid questionResponseId, Dal.Entities.Quest.Quest quest, QuestTaskResponseDTO response)
        {
            var questToComplete = await context.QuestResponses
                        .Include(x => x.TaskStatuses)
                        .FirstAsync(x => x.Id == questionResponseId);

            int xpGained;
            if (response.Success)
            {
                if (questToComplete.TaskStatuses.Any(x => x.Status == QuestTaskStatusEnum.Failed))
                {
                    questToComplete.Status = QuestResponseStatusEnum.Completed;
                    xpGained = quest.XpForComplete;
                }
                else
                {
                    questToComplete.Status = QuestResponseStatusEnum.CompletedSuccessfully;
                    xpGained = quest.XpForComplete + quest.XpForSuccess;
                }

                await accountService.AddXPAsync(xpGained);
            }
            else
            {
                questToComplete.Status = QuestResponseStatusEnum.Completed;
                xpGained = 0;
            }

            questToComplete.XpEarned = xpGained;
            questToComplete.CompletedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();

            response.IsFinished = true;
            return response;
        }
    }
}