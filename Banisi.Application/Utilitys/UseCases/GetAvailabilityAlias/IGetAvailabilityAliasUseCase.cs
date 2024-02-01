namespace Banisi.Application.Utilitys.UseCases.GetAvailabilityAlias
{
    public interface IGetAvailabilityAliasUseCase
    {
        Task Execute(string alias, string appVersion, string clientIp);
        void SetOutputPort(IOutputPort outputPort);
    }
}