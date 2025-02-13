﻿using BobrVerse.Bll.Interfaces.Quest.TaskValidator;
using BobrVerse.Common.Models.DTO.Quest.Task;
using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Entities.Quest.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Text;
using System.Text.RegularExpressions;

namespace BobrVerse.Bll.Services.Quest.TaskValidator
{
    public class CollectResourcesTaskValidator : IQuestTaskValidator
    {
        public TaskValidationState Validate(CreateQuestTaskResponseDTO dto, QuizTask task)
        {
            var response = new TaskValidationState();
            if (task.MaxCollectCalls != null)
            {
                if (CountCollectCalls(dto.Text!) > task.MaxCollectCalls)
                {
                    response.Success = false;
                    response.ErrorMessage = "Max number of calls of method collect exceeded.";
                    return response;
                }
            }
            var collector = new ResourceCollector();
            var resourcesNames = task.RequiredResources.Select(x => x.Name).ToHashSet();
            StringBuilder fullCode = new();
            foreach (var name in resourcesNames)
            {
                fullCode.AppendLine($@"var {name.ToLower()} = new Resource {{ Name = ""{name}"" }};");
            }

            fullCode.AppendLine(dto.Text);
            var scriptOptions = ScriptOptions.Default
                .WithReferences(typeof(ResourceCollector).Assembly)
                .WithImports("BobrVerse.Dal.Entities.Quest.Tasks");

            try
            {
                var script = CSharpScript.Create(fullCode.ToString(), scriptOptions, globalsType: typeof(ResourceCollector));
                script.RunAsync(globals: collector).Wait();
            }
            catch(Exception ex)
            {
                response.ErrorMessage = ex.Message;
                return response;
            }

            var skip = 0;
            foreach (var requiredResource in task.RequiredResources.OrderBy(x => x.Order))
            {
                var collected = collector.Resources.Skip(skip).Take(requiredResource.Quantity);
                if (collected.Any(x => x.Name != requiredResource.Name))
                {
                    response.ErrorMessage = $"Resources are collected in wrong count or order.";
                    return response;
                }

                skip += requiredResource.Quantity;
            }

            return response;
        }

        private int CountCollectCalls(string scriptText)
        {
            var regex = new Regex(@"\bcollect\b", RegexOptions.IgnoreCase);
            var matches = regex.Matches(scriptText);
            return matches.Count;
        }

        public class ResourceCollector
        {
            public class Resource
            {
                public string Name { get; set; } = string.Empty;
            }
            public List<Resource> Resources { get; } = [];
            public void collect(Resource resource) => Resources.Add(resource);
        }
    }
}
