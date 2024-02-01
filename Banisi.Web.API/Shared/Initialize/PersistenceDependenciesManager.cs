using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Persistence;

namespace Banisi.Web.API.Shared.Initialize
{
    public static class PersistenceDependenciesManager
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IDatabaseContext), typeof(DatabaseContext), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IRepository<,>), typeof(Repository<,>), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>), ServiceLifetime.Transient));

            return services;
        }
    }
}