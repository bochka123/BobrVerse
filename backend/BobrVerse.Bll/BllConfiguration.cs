using BobrVerse.Bll.Interfaces;
using BobrVerse.Bll.Mappers;
using BobrVerse.Bll.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BobrVerse.Bll
{
    public static class BllConfiguration
    {
        public static IServiceCollection ConfigureBllServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IBobrAccountService, BobrAccountService>();
            services.AddScoped<IBobrLevelService, BobrLevelService>();
            services.AddAutoMapper(typeof(MapperProfile));
            return services;
        }
    }
}
