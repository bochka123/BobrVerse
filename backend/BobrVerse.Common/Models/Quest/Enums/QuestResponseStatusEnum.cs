using System.ComponentModel;

namespace BobrVerse.Common.Models.Quest.Enums
{
    public enum QuestResponseStatusEnum
    {
        [Description("In Progress")]
        InProgress,
        [Description("Completed Successfully")]
        CompletedSuccessfully,
        Completed,
        [Description("Not Completed")]
        NotCompleted,
        [Description("Not Started")]
        NotStarted
    }
}
