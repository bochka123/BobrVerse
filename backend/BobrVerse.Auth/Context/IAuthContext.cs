using BobrVerse.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Auth.Context
{
    public interface IAuthContext
    {
        DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
