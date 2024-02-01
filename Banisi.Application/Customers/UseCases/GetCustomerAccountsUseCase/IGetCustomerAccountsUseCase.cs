namespace Banisi.Application.Customers.UseCases.GetCustomerAccountsUseCase
{
    public interface IGetCustomerAccountsUseCase
    {
        Task Execute();
        void SetOutputPort(IOutputPort outputPort);
    }
}