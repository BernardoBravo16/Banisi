namespace Banisi.Application.Shared.Contracts.Infrastructure
{
    public interface IStorageAdapterFactory
    {
        Task<IStorageAdapter> BuildAsync();
    }
}