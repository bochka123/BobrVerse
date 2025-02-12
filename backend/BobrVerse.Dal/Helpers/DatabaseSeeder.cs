using BobrVerse.Dal.Context;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Interfaces;

namespace BobrVerse.Dal.Seeders
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
                    new() { Id = Guid.NewGuid(), Level = 1, RequiredXP = 0, Title = "Young Beaver", Description = "You're just beginning your journey in the world of beavers. Your teeth are sharp, but do you have what it takes to build your first lodge?", LogsToAdd = 5 },
                    new() { Id = Guid.NewGuid(), Level = 2, RequiredXP = 100, Title = "Novice Beaver", Description = "You can build a lodge, but you still confuse pines with firs. Ready for new adventures?", LogsToAdd = 20 },
                    new() { Id = Guid.NewGuid(), Level = 3, RequiredXP = 250, Title = "Greta the Dam Maker", Description = "You know how to make dams, but sometimes your construction looks more like a slide. Keep learning—this is really important!", LogsToAdd = 45 },
                    new() { Id = Guid.NewGuid(), Level = 4, RequiredXP = 500, Title = "Beaver Builder", Description = "You've mastered all the building tricks! Now your dams are so strong that even foxes are jealous.", LogsToAdd = 90 },
                    new() { Id = Guid.NewGuid(), Level = 5, RequiredXP = 1000, Title = "Forest Architect", Description = "You're creating architectural masterpieces, but why does your dam keep breaking? Time for a scientific approach!", LogsToAdd = 170 },
                    new() { Id = Guid.NewGuid(), Level = 6, RequiredXP = 1500, Title = "Master of Sticks", Description = "You handle sticks so skillfully that you can make anything from them—from a lodge to a fish elevator!", LogsToAdd = 270 },
                    new() { Id = Guid.NewGuid(), Level = 7, RequiredXP = 2000, Title = "Leader of the Woodland Legion", Description = "You lead the beavers and know how to organize their work, even if others mistake you for a beaver general.", LogsToAdd = 400 },
                    new() { Id = Guid.NewGuid(), Level = 8, RequiredXP = 3000, Title = "Beaver Warrior", Description = "You're not just a builder, but a true beaver warrior! You command dams and scare off enemies with just a glance.", LogsToAdd = 620 },
                    new() { Id = Guid.NewGuid(), Level = 9, RequiredXP = 4000, Title = "Forest Witcher", Description = "You wield the magic of trees, and even ancient oaks listen to your word. But beware—forest spirits might throw some nasty surprises your way!", LogsToAdd = 900 },
                    new() { Id = Guid.NewGuid(), Level = 10, RequiredXP = 5000, Title = "Legendary Beaver", Description = "You are the living embodiment of beaver wisdom and strength. Your dams have become the stuff of forest legend!", LogsToAdd = 1300 },
                    new() { Id = Guid.NewGuid(), Level = 11, RequiredXP = 6000, Title = "Master of Dams", Description = "You're not just a builder, but a true master in the art of dam construction. Your fame has exceeded all expectations!", LogsToAdd = 1900 },
                    new() { Id = Guid.NewGuid(), Level = 12, RequiredXP = 7500, Title = "Beaver Demolisher", Description = "You know not only how to build but also how to tear down old structures! You have a real beaver excavator in your teeth.", LogsToAdd = 3000 },
                    new() { Id = Guid.NewGuid(), Level = 13, RequiredXP = 10000, Title = "Ruler of Dams", Description = "You have built the largest and strongest dam. Not even the biggest flood can break it!", LogsToAdd = 5000 },
                    new() { Id = Guid.NewGuid(), Level = 14, RequiredXP = 12000, Title = "Beaver Emperor of the Forest", Description = "You are the true emperor of the forest, and all beavers in the world want to be your subjects. Maybe even wolves will start respecting you!", LogsToAdd = 7000 },
                    new() { Id = Guid.NewGuid(), Level = 15, RequiredXP = 15000, Title = "Beaver Kurwa", Description = "You are the beaver even forest spirits fear. Your fame surpasses all, and your dams are not just constructions—they are real fortresses! You are the king among beavers!", LogsToAdd = 10000 },
                };

                _context.BobrLevels.AddRange(levels);
                await _context.SaveChangesAsync();
            }
        }
    }
}
