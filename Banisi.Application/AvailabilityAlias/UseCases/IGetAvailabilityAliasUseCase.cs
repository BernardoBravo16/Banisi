namespace Banisi.Application.AvailabilityAlias.UseCases
{
    public interface IGetAvailabilityAliasUseCase
    {
        Task Execute(string alias, string appVersion, string clientIp);
        void SetOutputPort(IOutputPort outputPort);
    }
}