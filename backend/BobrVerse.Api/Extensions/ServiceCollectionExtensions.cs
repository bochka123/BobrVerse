using BobrVerse.Api.Models.Settings;
using BobrVerse.Auth;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Helpers;
using BobrVerse.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BobrVerse.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBobrVerseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<AppSettings>(configuration);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value.Auth);
            services.AddScoped<IMigrationHelper, MigrationHelper>();
            services.AddDbContext<BobrVerseContext>(options => options.UseSqlServer(configuration.GetConnectionString("BobrVerseDb")));

            services.AddAuth();
            services.AddAuthDbContext<BobrVerseContext>();

            return services;
        }
    }
}
