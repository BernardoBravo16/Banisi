namespace Banisi.Application.Shared.Contracts.Persistence
{
    public interface IReadOnlyRepository<T, U>
           where T : class
           where U : struct
    {
        IQueryable<T> GetAll();
        T Get(U id);
        Task<T> GetAsync(U id);
    }
}