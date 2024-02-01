using Banisi.Web.API.Shared;
using Banisi.Application.Customers.UseCases.GetCustomerAccountsUseCase;
using Banisi.Application.ModelsYappy;

namespace Banisi.Web.API.Customers.Presenters
{
    public class GetCustomerAccountsPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ApiResponse apiResponse)
        {
            SetOkObject(apiResponse);
        }
    }
}