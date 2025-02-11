using System.ComponentModel;

namespace BobrVerse.Common.Models.Quiz.Enums
{
    public enum QuestTaskStatusEnum
    {
        [Description("Not Started")]
        NotStarted,
        [Description("In Progress")]
        InProgress,
        Failed,
        Completed
    }
}
