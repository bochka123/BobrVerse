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
                    new() { Id = Guid.NewGuid(), Level = 1, RequiredXP = 0, Title = "Молодий Бобер", Description = "Ти тільки починаєш свою подорож у світі бобрів. Твої зуби гострі, але чи вистачить тебе на побудову своєї першої хатини?" },
                    new() { Id = Guid.NewGuid(), Level = 2, RequiredXP = 100, Title = "Бобер-Початківець", Description = "Ти вже можеш будувати хатину, але все ще плутаєш сосни з ялинами. Готовий до нових пригод?" },
                    new() { Id = Guid.NewGuid(), Level = 3, RequiredXP = 250, Title = "Грета Гребель", Description = "Ти вмієш робити греблі, але інколи твоя конструкція більше схожа на гірку. Вчимося, бо насправді це дуже важливо!" },
                    new() { Id = Guid.NewGuid(), Level = 4, RequiredXP = 500, Title = "Бобер-Будівельник", Description = "Ти освоїв всі будівельні хитрощі! Тепер твої греблі настільки міцні, що їм заздрять навіть лисиці." },
                    new() { Id = Guid.NewGuid(), Level = 5, RequiredXP = 1000, Title = "Лісовий Архітектор", Description = "Ти створюєш архітектурні шедеври, але чому твоя гребля постійно ламається? Час для наукового підходу!" },
                    new() { Id = Guid.NewGuid(), Level = 6, RequiredXP = 1500, Title = "Палатний Бобер", Description = "Ти настільки майстерно володієш паличками, що з них можна зробити все: від хатини до ліфта для риб!" },
                    new() { Id = Guid.NewGuid(), Level = 7, RequiredXP = 2000, Title = "Вождь Лісової Легії", Description = "Ти очолюєш бобрів і знаєш, як організувати їх роботу, навіть якщо інші плутають тебе з бобровим військовим." },
                    new() { Id = Guid.NewGuid(), Level = 8, RequiredXP = 3000, Title = "Бобер-Бойовик", Description = "Ти не просто будівельник, а справжній бобровий воїн! Гріблями керуєш, а ворогів відлякуєш одним поглядом." },
                    new() { Id = Guid.NewGuid(), Level = 9, RequiredXP = 4000, Title = "Лісовий Відьмак", Description = "Ти володієш магією дерев, і навіть старі дуби прислухаються до твого слова. Але будь обережний — лісові духи можуть підкинути тобі пару неприємних сюрпризів!" },
                    new() { Id = Guid.NewGuid(), Level = 10, RequiredXP = 5000, Title = "Легендарний Бобер", Description = "Ти - живе втілення бобрячої мудрості та сили. Твої греблі стали легендою лісу!" },
                    new() { Id = Guid.NewGuid(), Level = 11, RequiredXP = 6000, Title = "Бобер-Магістр Гребель", Description = "Ти не просто будівельник, а магістр у мистецтві створення гребель. Твоя слава перевершила усі очікування!" },
                    new() { Id = Guid.NewGuid(), Level = 12, RequiredXP = 7500, Title = "Бобер-Руйнатор", Description = "Ти вмієш не лише будувати, а й руйнувати старі конструкції! У тебе є справжній бобровий екскаватор в зубах." },
                    new() { Id = Guid.NewGuid(), Level = 13, RequiredXP = 10000, Title = "Володар Гребель", Description = "Ти побудував найбільшу та найміцнішу греблю. Її не зламає навіть найбільша повінь!" },
                    new() { Id = Guid.NewGuid(), Level = 14, RequiredXP = 12000, Title = "Бобер-Імператор Лісу", Description = "Ти — справжній імператор лісу, і всі бобри у світі хочуть бути твоїми підданими. Можливо, навіть вовки почнуть тебе шанувати!" },
                    new() { Id = Guid.NewGuid(), Level = 15, RequiredXP = 15000, Title = "Бобр Курва", Description = "Ти — бобр, якого навіть лісові духи бояться. Твоя слава перевершує все, а твої греблі — це не просто конструкції, а справжні фортеці! Ти — король серед бобрів!" },
                };

                _context.BobrLevels.AddRange(levels);
                await _context.SaveChangesAsync();
            }
        }
    }
}
