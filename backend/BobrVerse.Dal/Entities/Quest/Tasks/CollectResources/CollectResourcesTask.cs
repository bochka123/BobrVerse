namespace BobrVerse.Dal.Entities.Quest.Tasks.CollectResources
{
    public class CollectResourcesTask : QuizTask
    {
        public List<Resource> RequiredResources { get; set; } = [];
        public string CodeTemplate { get; set; } = string.Empty;
    }
}
