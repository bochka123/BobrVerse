using BobrVerse.Auth.Context;
using BobrVerse.Auth.Entities;
using BobrVerse.Common.Models.Quiz.Enums;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;
using BobrVerse.Dal.Entities.Quest.Tasks;
using BobrVerse.Dal.Entities.Quest.Tasks.CollectResources;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Dal.Context
{
    public class BobrVerseContext(DbContextOptions<BobrVerseContext> options) : DbContext(options), IAuthContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BobrProfile> BobrProfiles { get; set; }
        public DbSet<BobrLevel> BobrLevels { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestResponse> QuestResponses {  get; set; }
        public DbSet<QuizTaskStatus> QuizTaskStatuses {  get; set; }
        public DbSet<QuizTask> QuizTasks { get; set; }
        public DbSet<Resource> Resources {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QuizTask>()
                .HasDiscriminator<TaskTypeEnum>(nameof(QuizTask.TaskType))
                .HasValue<CollectResourcesTask>(TaskTypeEnum.CollectResources);

            modelBuilder.Entity<BobrProfile>()
                .HasOne(bp => bp.User)
                .WithOne()
                .HasForeignKey<BobrProfile>(bp => bp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BobrProfile>()
                .HasOne(bp => bp.Level)
                .WithMany()
                .HasForeignKey(bp => bp.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Quest>()
                .HasOne(q => q.Author)
                .WithMany(bp => bp.CreatedQuests)
                .HasForeignKey(q => q.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Quest>()
                .HasMany(q => q.QuestResponses)
                .WithOne(qr => qr.Quest)
                .HasForeignKey(qr => qr.QuestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
