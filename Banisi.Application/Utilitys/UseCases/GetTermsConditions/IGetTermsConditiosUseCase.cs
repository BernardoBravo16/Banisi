namespace Banisi.Application.Utilitys.UseCases.GetTermsConditions
{
    public interface IGetTermsConditiosUseCase
    {
        Task Execute(string appVersion, string clientIp);
        void SetOutputPort(IOutputPort outputPort);
    }
}