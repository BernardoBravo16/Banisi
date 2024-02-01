using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Banisi.Persistence
{
    public class Repository<T, U> : IRepository<T, U>
        where T : class, IGenericEntity<U>
        where U : struct
    {
        private readonly IDatabaseContext _database;

        public Repository(IDatabaseContext databaseContext)
        {
            _database = databaseContext;
        }

        public void Add(T entity)
        {
            _database.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _database.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(ICollection<T> entity)
        {
            await _database.Set<T>().AddRangeAsync(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _database.Set<T>();
        }

        public void Remove(T entity)
        {
            _database.Set<T>().Remove(entity);
        }

        public T Get(U id)
        {
            var convertedId = (U)id;
            return _database.Set<T>()
                .FirstOrDefault(p => p.Id.Equals(convertedId));
        }

        public async Task<T> GetAsync(U id)
        {
            var convertedId = (object)id;
            return await _database.Set<T>()
                .FirstOrDefaultAsync(p => p.Id.Equals(convertedId));
        }
    }
}