using BobrVerse.Dal.Context;
using BobrVerse.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Dal.Helpers
{
    public class MigrationHelper(BobrVerseContext context) : IMigrationHelper
    {
        public void Migrate()
        {
            if (context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
