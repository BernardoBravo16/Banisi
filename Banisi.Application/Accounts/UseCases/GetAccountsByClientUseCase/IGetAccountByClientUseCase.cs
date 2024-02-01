namespace Banisi.Application.Accounts.UseCases.GetAccountsByClientUseCase
{
    public interface IGetAccountByClientUseCase
    {
        Task Execute(int clientId, string clientIp);
        void SetOutputPort(IOutputPort outputPort);
    }
}