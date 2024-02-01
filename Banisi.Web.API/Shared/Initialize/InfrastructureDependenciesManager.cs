using Banisi.Application.Shared.Contracts.Infrastructure;

namespace Banisi.Web.API.Shared.Initialize
{
    public static class InfrastructureDependenciesManager
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddTransient<IStorageAdapterFactory, StorageAdapterFactory>();

            return services;
        }
    }
}