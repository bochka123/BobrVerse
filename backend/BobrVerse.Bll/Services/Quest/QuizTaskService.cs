using AutoMapper;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities.Quest.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuizTaskService(BobrVerseContext context, IMapper mapper) : IQuizTaskService
    {
        public async Task CreateAsync(CreateTaskDTO dto)
        {
            var dbModel = mapper.Map<QuizTask>(dto);
            ValidateByTaskType(dbModel);
           
            var quest = await context.Quests.FirstOrDefaultAsync(x => x.Id == dto.QuestId)
                ?? throw new BobrException($"Quest is not found with id {dto.QuestId}");

            quest.Tasks.Add(dbModel);
            await context.SaveChangesAsync();
        }

        private void ValidateByTaskType(QuizTask task)
        {
            if (task.TaskType == Common.Models.Quiz.Enums.TaskTypeEnum.CollectResources)
            {
                if (task.RequiredResources.Count == 0 || string.IsNullOrEmpty(task.CodeTemplate))
                {
                    throw new BobrException("Invlid arguments for creating task.");
                }
            }
            else
            {
                throw new BobrException("Task type is invalid.");
            }
        }
    }
}
