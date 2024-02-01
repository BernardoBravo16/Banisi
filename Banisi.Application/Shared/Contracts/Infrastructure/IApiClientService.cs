using Banisi.Application.Shared.Models.Base;
using System.Threading.Tasks;

namespace Banisi.Application.Shared.Contracts.Infrastructure
{
    public interface IApiClientService
    {
        Task<ServiceResponse> GetAsync<T>(string url, Dictionary<string, string> headers = null) where T : class;
        Task<ServiceResponse> PostAsync<T>(string url, T data, Dictionary<string, string> headers = null) where T : class;
        Task<ServiceResponse> PutAsync<T>(string url, T data) where T : class;
        Task<ServiceResponse> DeleteAsync<T>(string url);
    }
}