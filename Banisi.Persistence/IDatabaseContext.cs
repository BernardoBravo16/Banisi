using Banisi.Domain.Entities.Affiliations;
using Microsoft.EntityFrameworkCore;

namespace Banisi.Persistence
{
    public interface IDatabaseContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbSet<T> Set<T>() where T : class;
    }
}
