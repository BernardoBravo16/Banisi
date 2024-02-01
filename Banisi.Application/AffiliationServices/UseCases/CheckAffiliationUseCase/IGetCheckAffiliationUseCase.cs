namespace Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase
{
    public interface IGetCheckAffiliationUseCase
    {
        Task Execute(int clientId);
        void SetOutputPort(IOutputPort outputPort);
    }
}