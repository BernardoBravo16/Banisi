using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Banisi.Persistence
{
    public class ReadOnlyRepository<T, U> : IReadOnlyRepository<T, U>
        where T : class, IGenericEntity<U>
        where U : struct
    {
        private readonly IDatabaseContext _database;

        public ReadOnlyRepository(IDatabaseContext databaseContext)
        {
            _database = databaseContext;
        }

        public IQueryable<T> GetAll()
        {
            return _database.Set<T>()
                .AsNoTracking();
        }

        public T Get(U id)
        {
            var convertedId = (U)id;
            return _database.Set<T>()
                .AsNoTracking()
                .FirstOrDefault(p => p.Id.Equals(convertedId));
        }

        public async Task<T> GetAsync(U id)
        {
            var convertedId = (object)id;
            return await _database.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id.Equals(convertedId));
        }
    }
}