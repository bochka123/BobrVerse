using AutoMapper;
using BobrVerse.Auth.Services;
using BobrVerse.Bll.Interfaces;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Extenions;
using BobrVerse.Common.Models.DTO.File;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities.Quest.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Bll.Services.Quest
{
    public class QuizTaskService(BobrVerseContext context, IMapper mapper, IAzureBlobStorageService azureBlobStorageService) : IQuizTaskService
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
                ?? throw new BobrException($"Task with id {Id} not found.");

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
                if (task.RequiredResources.Count == 0)
                {
                    throw new BobrException("Invlid arguments for creating task.");
                }
            }
            else
            {
                throw new BobrException("Task type is invalid.");
            }
        }

        private async void UpdateRequiredResources(ICollection<Resource> existingResources, List<ResourceDTO> dtoResources, IMapper mapper)
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
                    await context.Resources.AddAsync(newResource);
                }
            }
        }

        public async Task<QuizTaskDTO?> GetByOrderAsync(Guid questId, int order)
        {
            var dbModel = await context.QuizTasks.Include(x => x.RequiredResources).FirstOrDefaultAsync(x => x.QuestId == questId && x.Order == order);

            return dbModel is null ? null : mapper.Map<QuizTaskDTO>(dbModel);
        }

        public async Task<QuizTaskDTO> GetByIdAsync(Guid taskId)
        {
            var dbModel = await context.QuizTasks.AsNoTracking().Include(x => x.RequiredResources).FirstOrDefaultAsync(x => x.Id == taskId) 
                ?? throw new BobrException("Quest task doesn't exist.");

            var task = mapper.Map<QuizTaskDTO>(dbModel);

            var nextTask = await context.QuizTasks.AsNoTracking().FirstOrDefaultAsync(x => x.QuestId == dbModel.QuestId && x.Order == dbModel.Order);

            task.NextTaskId = nextTask?.Id;

            return task;
        }

        public async Task<FileDto> UploadPhotoAsync(IFormCollection formCollection, Guid questTaskId)
        {
            var file = formCollection.Files.FirstOrDefault();
            var newFileDto = new NewFileDto()
            {
                Stream = file.OpenReadStream(),
                FileName = file.FileName
            };

            var questTask = await context.QuizTasks.FirstOrDefaultAsync(x => x.Id == questTaskId)
                ?? throw new InvalidOperationException("Quest task doesn't exist.");

            if (!string.IsNullOrEmpty(questTask.Url))
            {
                var oldFile = new FileDto()
                {
                    Url = questTask.Url
                };

                await azureBlobStorageService.DeleteFromBlob(oldFile);
            }

            var fileDto = await azureBlobStorageService.AddFileToBlobStorage(newFileDto);

            questTask.Url = fileDto.Url;
            await context.SaveChangesAsync();
            return fileDto;
        }

        public async Task<bool> DeletePhotoAsync(Guid questTaskId)
        {
            var questTask = await context.QuizTasks.FirstOrDefaultAsync(x => x.Id == questTaskId)
                ?? throw new InvalidOperationException("Quest task doesn't exist.");

            var oldFile = new FileDto()
            {
                Url = questTask.Url
            };

            await azureBlobStorageService.DeleteFromBlob(oldFile);

            return true;
        }

        public ICollection<QuestTaskTypeInfoDTO> GetInfoForCreatingTasks()
        {
            return
            [
                new QuestTaskTypeInfoDTO
                {
                    Name = TaskTypeEnum.CollectResources.GetDescription(),
                    TaskType = TaskTypeEnum.CollectResources.ToString(),
                    Description = "This task requires the collection of resources (e.g., Rock, Wood) in specific quantities and order. The 'collect()' method can be used to collect resources in the correct sequence and quantity as specified. Use c# syntax. Cycles are also suuported.",
                    Parameters = new Dictionary<string, string>
                    {
                        { nameof(QuizTask.MaxCollectCalls), "The maximum number of times the collect method can be called." },
                        { nameof(QuizTask.RequiredResources), "A list of required resources (e.g., Rock, Wood) to be collected." }
                    }
                },
                new QuestTaskTypeInfoDTO{
                    Name = TaskTypeEnum.CutTreesInForest.GetDescription(),
                    TaskType = TaskTypeEnum.CutTreesInForest.ToString(),
                    Description = "This task requires cutting down a specified number of trees in a forest. The user will specify the total forest size and the number of trees to cut, choosing whether to cut the largest or smallest trees. Use the `cut()` method to perform the action. Array of trees is variable 'Forest', every tree has property 'Length'.",
                    Parameters = new Dictionary<string, string>
                    {
                        { nameof(QuizTask.ForestSize), "The total number of trees in the forest." },
                        { nameof(QuizTask.TreesToCut), "The number of trees that need to be cut down." },
                        { nameof(QuizTask.CutLargest), "Determines whether to cut the largest (true) or smallest (false) trees." }
                    }
                }
            ];
        }
    }
}
