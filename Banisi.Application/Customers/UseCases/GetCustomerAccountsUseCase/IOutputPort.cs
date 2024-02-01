using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Customers.UseCases.GetCustomerAccountsUseCase
{
    public interface IOutputPort
    {
        public void Ok(ApiResponse apiResponse);
    }
}