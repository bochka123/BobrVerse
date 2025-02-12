using BobrVerse.Bll.Interfaces.Quest.TaskValidator;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Dal.Entities.Quest.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace BobrVerse.Bll.Services.Quest.TaskValidator
{
    public class CutTreesInForestTaskValidator : IQuestTaskValidator
    {
        public TaskValidationState Validate(CreateQuestTaskResponseDTO dto, QuizTask task)
        {
            var response = new TaskValidationState();
            var collector = new ResourceCollector(task.ForestSize!.Value);
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
                response.ErrorMessage = ex.Message;
                return response;
            }

            var take = task.TreesToCut!.Value;
            var selectedTrees = task.CutLargest!.Value
                ? collector.Forest.OrderByDescending(r => r.Length).Take(take).ToList()
                : collector.Forest.OrderBy(r => r.Length).Take(take).ToList();

            response.Success = collector.Resources
                .OrderBy(r => r.Length)
                .SequenceEqual(selectedTrees.OrderBy(r => r.Length), new ResourceComparer());

            return response;
        }

        public class ResourceCollector(int massiveSize)
        {
            private static Random random = new Random();
            public List<Resource> Forest { get; } = Enumerable.Range(0, massiveSize)
                                  .Select(_ => new Resource { Length = random.Next(1, 101) })
                                  .ToList();

            public class Resource
            {
                public int Length { get; set; }
            }
            public List<Resource> Resources { get; } = [];
            public void cut(Resource resource) => Resources.Add(resource);
        }

        public class ResourceComparer : IEqualityComparer<ResourceCollector.Resource>
        {
            public bool Equals(ResourceCollector.Resource? x, ResourceCollector.Resource? y) =>
                x?.Length == y?.Length;

            public int GetHashCode(ResourceCollector.Resource obj) => obj.Length.GetHashCode();
        }

    }
}
