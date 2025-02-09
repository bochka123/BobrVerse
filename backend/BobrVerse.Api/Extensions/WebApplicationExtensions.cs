using BobrVerse.Dal.Interfaces;

namespace BobrVerse.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var migrationHelper = scope.ServiceProvider.GetRequiredService<IMigrationHelper>();
            migrationHelper.Migrate();

            var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
            seeder.SeedAsync().GetAwaiter().GetResult();
        }
    }
}
