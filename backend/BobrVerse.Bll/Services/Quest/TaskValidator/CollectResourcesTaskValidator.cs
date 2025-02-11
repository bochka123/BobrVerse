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
            var preCode = @"
            var wood = new Wood();
            var rock = new Rock();
            ";
            var fullScript = preCode + dto.Text;
            var scriptOptions = ScriptOptions.Default
                .WithReferences(typeof(ResourceCollector).Assembly)
                .WithImports("BobrVerse.Dal.Entities.Quest.Tasks");

            try
            {
                var script = CSharpScript.Create(fullScript, scriptOptions, globalsType: typeof(ResourceCollector));
                script.RunAsync(globals: collector).Wait();
            }
            catch
            {
                return false;
            }

            foreach (var requiredResource in task.RequiredResources)
            {
                switch (requiredResource.Name)
                {
                    case Common.Models.Quiz.Enums.ResourceNameEnum.Rock:
                        if (collector.Rocks.Count != requiredResource.Quantity)
                            return false;
                        break;

                    case Common.Models.Quiz.Enums.ResourceNameEnum.Wood:
                        if (collector.Woods.Count != requiredResource.Quantity)
                            return false;
                        break;
                }
            }

            return true;
        }

        
        public class ResourceCollector
        {
            public class Rock 
            {
                public int Weight { get; set; }
            }
            public class Wood 
            {
                public int Length { get; set; }
            }
            public List<Rock> Rocks { get; } = [];
            public List<Wood> Woods { get; } = [];

            public void collect(Rock rock) => Rocks.Add(rock);
            public void collect(Wood wood) => Woods.Add(wood);
        }
    }
}
