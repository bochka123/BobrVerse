using BobrVerse.Auth.Context;
using BobrVerse.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Dal.Context
{
    public class BobrVerseContext(DbContextOptions<BobrVerseContext> options) : DbContext(options), IAuthContext
    {
        public DbSet<User> Users { get; set; }
    }
}
