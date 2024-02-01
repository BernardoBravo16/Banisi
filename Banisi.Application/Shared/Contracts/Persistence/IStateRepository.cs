namespace Banisi.Application.Shared.Contracts.Persistence
{
    public interface IStateRepository<T> where T : class
    {
        Task Put(Task entity);
        Task<T> Get(object id);
    }
}