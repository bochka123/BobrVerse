using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Interfaces;

namespace BobrVerse.Api.Seeders
{
    public class DatabaseSeeder(BobrVerseContext context) : IDatabaseSeeder
    {
        private readonly BobrVerseContext _context = context;

        public async Task SeedAsync()
        {
            if (!_context.BobrLevels.Any())
            {
                var levels = new List<BobrLevel>
                {
                    new() { Id = Guid.NewGuid(), Level = 1, RequiredXP = 0, Title = "Молодий Бобер" },
                    new() { Id = Guid.NewGuid(), Level = 2, RequiredXP = 100, Title = "Дослідник Лісу" },
                    new() { Id = Guid.NewGuid(), Level = 3, RequiredXP = 250, Title = "Бобер-Воєвода" },
                    new() { Id = Guid.NewGuid(), Level = 4, RequiredXP = 500, Title = "Верховний Інженер" },
                    new() { Id = Guid.NewGuid(), Level = 5, RequiredXP = 1000, Title = "Легендарний Бобер" }
                };

                _context.BobrLevels.AddRange(levels);
                await _context.SaveChangesAsync();
            }
        }
    }
}
