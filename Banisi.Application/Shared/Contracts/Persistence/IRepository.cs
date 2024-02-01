namespace Banisi.Application.Shared.Contracts.Persistence
{
    public interface IRepository<T, U>
        where T : class
        where U : struct
    {
        IQueryable<T> GetAll();
        T Get(U id);
        Task<T> GetAsync(U id);
        void Add(T entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entity);
        void Remove(T entity);
    }
}