using Banisi.Web.API.Shared;
using Banisi.Application.Customers.UseCases.CreateCustomerSeedUseCase;
using Banisi.Application.ModelsYappy;
using Azure;

namespace Banisi.Web.API.Customers.Presenters
{
    public class CreateCustomerSeedPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ApiResponse apiResponse)
        {
            SetOkObject(apiResponse);
        }
    }
}