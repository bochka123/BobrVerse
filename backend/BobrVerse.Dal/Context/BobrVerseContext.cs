using BobrVerse.Auth.Context;
using BobrVerse.Auth.Entities;
using BobrVerse.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Dal.Context
{
    public class BobrVerseContext(DbContextOptions<BobrVerseContext> options) : DbContext(options), IAuthContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BobrProfile> BobrProfiles { get; set; }
        public DbSet<BobrLevel> BobrLevels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BobrProfile>()
                .HasOne(bp => bp.User)
                .WithOne()
                .HasForeignKey<BobrProfile>(bp => bp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BobrProfile>()
                .HasOne(bp => bp.Level)
                .WithMany()
                .HasForeignKey(bp => bp.LevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
