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
        public async Task<QuizTaskDTO> CreateAsync(CreateTaskDTO dto)
        {
            var dbModel = mapper.Map<QuizTask>(dto);
            dbModel.Id = Guid.NewGuid();
            ValidateByTaskType(dbModel);

            var quest = await context.Quests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == dto.QuestId)
                ?? throw new BobrException($"Quest is not found with id {dto.QuestId}");

            await context.QuizTasks.AddAsync(dbModel);
            await context.SaveChangesAsync();
            return mapper.Map<QuizTaskDTO>(dbModel);
        }
        public async Task<QuizTaskDTO> UpdateAsync(QuizTaskDTO dto)
        {
            var dbModel = await context.QuizTasks.Include(x => x.TaskStatuses).Include(x => x.RequiredResources).FirstOrDefaultAsync(x => x.Id == dto.Id)
                ?? throw new BobrException($"Task with id {dto.Id} not found.");

            if (dbModel.TaskStatuses.Count != 0)
            {
                throw new BobrException("Task cannot be updated with answers.");
            }

            mapper.Map(dto, dbModel);
            UpdateRequiredResources(dbModel.RequiredResources, dto.RequiredResources, mapper);
            ValidateByTaskType(dbModel);
            await context.SaveChangesAsync();
            return mapper.Map<QuizTaskDTO>(dbModel);
        }
        public async Task DeleteAsync(Guid Id)
        {
            var dbModel = await context.QuizTasks.FirstOrDefaultAsync(x => x.Id == Id)
                ?? throw new BobrException($"Task with id {Id} not found."); ;

            if (dbModel.TaskStatuses.Count != 0)
            {
                throw new BobrException("Task cannot be deleted with answers.");
            }

            context.QuizTasks.Remove(dbModel);
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

        private void UpdateRequiredResources(ICollection<Resource> existingResources, List<ResourceDTO> dtoResources, IMapper mapper)
        {
            var existingResourceIds = existingResources.Select(r => r.Id).ToHashSet();
            var dtoResourceIds = dtoResources.Select(r => r.Id).Where(id => id != Guid.Empty).ToHashSet();

            var resourcesToRemove = existingResources.Where(r => !dtoResourceIds.Contains(r.Id)).ToList();
            foreach (var resource in resourcesToRemove)
            {
                existingResources.Remove(resource);
            }

            foreach (var resourceDto in dtoResources)
            {
                if (resourceDto.Id != Guid.Empty && existingResourceIds.Contains(resourceDto.Id!.Value))
                {
                    var existingResource = existingResources.First(r => r.Id == resourceDto.Id);
                    mapper.Map(resourceDto, existingResource);
                }
                else
                {
                    var newResource = mapper.Map<Resource>(resourceDto);
                    existingResources.Add(newResource);
                }
            }
        }

        public async Task<QuizTaskDTO> GetByOrderAsync(Guid questId, int order)
        {
            var dbModel = await context.QuizTasks.Include(x => x.RequiredResources).FirstOrDefaultAsync(x => x.QuestId == questId && x.Order == order);

            return dbModel is null ? null : mapper.Map<QuizTaskDTO>(dbModel);
        }
    }
}
