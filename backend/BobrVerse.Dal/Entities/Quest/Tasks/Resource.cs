using BobrVerse.Common.Models.Quiz.Enums;

namespace BobrVerse.Dal.Entities.Quest.Tasks
{
    public class Resource
    {
        public Guid Id { get; set; }
        public ResourceNameEnum Name { get; set; }
        public int Quantity { get; set; }
        public int? Length { get; set; }
        public int? Weigth { get; set; }
    }
}
