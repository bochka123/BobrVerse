using BobrVerse.Auth.Context;
using BobrVerse.Auth.Entities;
using BobrVerse.Dal.Entities;
using BobrVerse.Dal.Entities.Quest;
using BobrVerse.Dal.Entities.Quest.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Dal.Context
{
    public class BobrVerseContext(DbContextOptions<BobrVerseContext> options) : DbContext(options), IAuthContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BobrProfile> BobrProfiles { get; set; }
        public DbSet<BobrLevel> BobrLevels { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestResponse> QuestResponses { get; set; }
        public DbSet<QuizTaskStatus> QuizTaskStatuses { get; set; }
        public DbSet<QuizTask> QuizTasks { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<QuestRating> QuestRatings { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuizTask>()
                .HasMany(qt => qt.RequiredResources)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity<Quest>()
                .HasMany(q => q.Tasks)
                .WithOne(t => t.Quest)
                .HasForeignKey(t => t.QuestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestRating>()
                .HasOne(qr => qr.QuestResponse)
                .WithOne(qr => qr.QuestRating)
                .HasForeignKey<QuestRating>(qr => qr.QuestResponseId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<QuestResponse>()
                .HasOne(qr => qr.QuestRating)
                .WithOne(qr => qr.QuestResponse)
                .HasForeignKey<QuestResponse>(qr => qr.QuestRatingId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<QuestRating>()
                .HasOne(qr => qr.BobrProfile)
                .WithMany(bp => bp.QuestRatings)
                .HasForeignKey(qr => qr.BobrProfileId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<QuestRating>()
                .HasOne(qr => qr.Quest)
                .WithMany(q => q.QuestRatings)
                .HasForeignKey(qr => qr.QuestId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
