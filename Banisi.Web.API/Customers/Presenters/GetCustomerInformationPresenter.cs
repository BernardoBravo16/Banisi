using Banisi.Application.ModelsYappy;
using Banisi.Web.API.Shared;
using Banisi.Application.Customers.UseCases.GetCustomerInformationUseCase;

namespace Banisi.Web.API.Customers.Presenters
{
    public class GetCustomerInformationPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ApiResponse apiResponse)
        {
            SetOkObject(apiResponse);
        }
    }
}
