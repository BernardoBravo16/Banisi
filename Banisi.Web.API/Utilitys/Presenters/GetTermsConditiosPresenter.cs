using Banisi.Application.Utilitys.UseCases.GetTermsConditions;
using Banisi.Application.Shared.Models.Base;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Utilitys.Presenters
{
    public class GetTermsConditiosPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ServiceResponse response)
        {
            SetOkObject(response);
        }
    }
}
