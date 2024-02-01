using Microsoft.Extensions.Logging;

namespace Banisi.Application.Shared.Contracts.Infrastructure
{
    public interface IStorageAdapter
    {
        ILogger Logger { get; set; }
    }
}