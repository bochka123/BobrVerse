using BobrVerse.Bll.Interfaces;
using BobrVerse.Bll.Interfaces.Quest;
using BobrVerse.Bll.Interfaces.Quest.TaskValidator;
using BobrVerse.Bll.Mappers;
using BobrVerse.Bll.Services;
using BobrVerse.Bll.Services.Quest;
using BobrVerse.Bll.Services.Quest.TaskValidator;
using Microsoft.Extensions.DependencyInjection;

namespace BobrVerse.Bll
{
    public static class BllConfiguration
    {
        public static IServiceCollection ConfigureBllServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IBobrAccountService, BobrAccountService>();
            services.AddScoped<IBobrLevelService, BobrLevelService>();
            services.AddScoped<IQuestService, QuestService>();
            services.AddScoped<IQuizTaskService, QuizTaskService>();
            services.AddScoped<IQuestRatingService, QuestRatingService>();
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
            services.AddTransient<IQuestTaskResponseService, QuestTaskResponseService>();
            services.AddTransient<IQuestTaskValidatorFactory, QuestTaskValidatorFactory>();
            services.AddAutoMapper(typeof(MapperProfile));
            return services;
        }
    }
}
