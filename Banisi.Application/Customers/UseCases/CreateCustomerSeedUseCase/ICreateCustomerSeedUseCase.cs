namespace Banisi.Application.Customers.UseCases.CreateCustomerSeedUseCase
{
    public interface ICreateCustomerSeedUseCase
    {
        Task Execute(CustomerSeedRequest model);
        void SetOutputPort(IOutputPort outputPort);
    }
}