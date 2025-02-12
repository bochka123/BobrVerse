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

            var maxValue = task.

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
    }
}
