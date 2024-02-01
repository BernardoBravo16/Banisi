using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Customers.UseCases.GetCustomerInformationUseCase
{
    public interface IOutputPort
    {
        public void Ok(ApiResponse apiResponse);
    }
}