using Azure.Storage.Blobs;
using BobrVerse.Api.Hubs;
using BobrVerse.Api.Models.Settings;
using BobrVerse.Api.Seeders;
using BobrVerse.Api.Services;
using BobrVerse.Auth;
using BobrVerse.Bll;
using BobrVerse.Bll.Interfaces.Hubs;
using BobrVerse.Common.Helpers;
using BobrVerse.Dal.Context;
using BobrVerse.Dal.Helpers;
using BobrVerse.Dal.Interfaces;
using Microsoft.AspNetCore.SignalR;
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(configuration.GetValue<string>("AllowedOrigins") ?? "")
                        .AllowCredentials()
                        .AllowAnyMethod()
                    .AllowAnyHeader());

                var allowedOrigin = configuration.GetValue<string>("AllowedOrigins") ?? "";

                options.AddPolicy("SignalRCorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => origin == allowedOrigin)
                        .AllowCredentials());

            });
            services.AddHttpClient();
            services.Configure<AppSettings>(configuration);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value.Auth);
            services.AddScoped<IMigrationHelper, MigrationHelper>();
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
            services.AddScoped<IQuestsHubService, QuestsHubService>();
            services.AddDbContext<BobrVerseContext>(options => options.UseSqlServer(configuration.GetConnectionString("BobrVerseDb")));
            services.ConfigureBllServiceCollection();
            services.AddSignalR().AddJsonProtocol();

            services.AddAuth();
            services.AddAuthDbContext<BobrVerseContext>();

            return services;
        }

        public static IServiceCollection AddBobrVerseAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorageSettings:ConnectionString"];

            services.AddScoped(_ =>
                    new BlobServiceClient(connectionString));

            services.AddSingleton(provider =>
            {
                var options = new BlobContainerOptionsHelper
                {
                    BlobContainerName = configuration["AzureBlobStorageSettings:BlobContainerName"]!
                };
                return options;
            });

            return services;
        }
    }
}
