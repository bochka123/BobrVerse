﻿namespace BobrVerse.Common.Models.DTO.Quest.Task
{
    public class ResourceDTO
    {
        public Guid? Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Order {  get; set; }
    }
}
