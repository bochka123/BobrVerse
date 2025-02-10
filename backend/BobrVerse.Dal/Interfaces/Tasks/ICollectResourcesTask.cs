using BobrVerse.Dal.Entities.Quest.Tasks;

namespace BobrVerse.Dal.Interfaces.Tasks
{
    public interface ICollectResourcesTask
    {
        ICollection<Resource> RequiredResources { get; set; }
        string? CodeTemplate { get; set; }
    }
}
