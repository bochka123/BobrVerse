using BobrVerse.Bll.Interfaces.Quest.TaskValidator;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Dal.Entities.Quest.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace BobrVerse.Bll.Services.Quest.TaskValidator
{
    public class CollectResourcesTaskValidator : IQuestTaskValidator
    {
        public bool Validate(CreateQuestTaskResponseDTO dto, QuizTask task)
        {
            var collector = new ResourceCollector();

            var scriptOptions = ScriptOptions.Default
                .WithReferences(typeof(ResourceCollector).Assembly)
                .WithImports("BobrVerse.Dal.Entities.Quest.Tasks");

            try
            {
                var script = CSharpScript.Create(dto.Text, scriptOptions, globalsType: typeof(ResourceCollector));
                script.RunAsync(globals: collector).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка виконання коду: {ex.Message}");
                return false;
            }

            foreach (var requiredResource in task.RequiredResources)
            {
                switch (requiredResource.Name)
                {
                    case Common.Models.Quiz.Enums.ResourceNameEnum.Rock:
                        if (collector.Rocks.Count < requiredResource.Quantity)
                            return false;
                        break;

                    case Common.Models.Quiz.Enums.ResourceNameEnum.Wood:
                        if (collector.Woods.Count < requiredResource.Quantity)
                            return false;
                        break;

                    default:
                        throw new ArgumentException($"Невідомий тип ресурсу: {requiredResource.Name}");
                }
            }

            return true;
        }

        public struct Rock { }
        public struct Wood { }
        public class ResourceCollector
        {
            public List<Rock> Rocks { get; } = [];
            public List<Wood> Woods { get; } = [];

            public void Collect(Rock rock) => Rocks.Add(rock);
            public void Collect(Wood wood) => Woods.Add(wood);
        }
    }
}
