using BobrVerse.Common.Models.Quiz.Enums;

namespace BobrVerse.Bll.Interfaces.Quest.TaskValidator
{
    public interface IQuestTaskValidatorFactory
    {
        public IQuestTaskValidator GetValidator(TaskTypeEnum taskType);
    }
}
