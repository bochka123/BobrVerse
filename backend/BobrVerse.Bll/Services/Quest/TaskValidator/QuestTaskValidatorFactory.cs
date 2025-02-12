using BobrVerse.Bll.Interfaces.Quest.TaskValidator;
using BobrVerse.Common.Exceptions;
using BobrVerse.Common.Models.Quiz.Enums;

namespace BobrVerse.Bll.Services.Quest.TaskValidator
{
    public class QuestTaskValidatorFactory : IQuestTaskValidatorFactory
    {
        public IQuestTaskValidator GetValidator(TaskTypeEnum taskType)
        {
            return taskType switch
            {
                TaskTypeEnum.CollectResources => new CollectResourcesTaskValidator(),
                TaskTypeEnum.CutTreesInForest => new CutTreesInForestTaskValidator(),
                _ => throw new BobrException($"Unsupported task type {taskType}.")
            };
        }
    }
}
