namespace Banisi.Application.Customers.UseCases.GetCustomerInformationUseCase
{
    public interface IGetCustomerInformationUseCase
    {
        Task Execute();
        void SetOutputPort(IOutputPort outputPort);
    }
}