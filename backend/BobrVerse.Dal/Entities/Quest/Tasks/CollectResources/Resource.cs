using BobrVerse.Common.Models.Quiz.Enums;

namespace BobrVerse.Dal.Entities.Quest.Tasks.CollectResources
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public ResourceTypeEnum Type { get; set; }
        public int? Length { get; set; }
        public int? Weigth { get; set; }
    }
}
