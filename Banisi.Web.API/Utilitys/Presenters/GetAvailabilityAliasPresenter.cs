using Banisi.Application.Utilitys.UseCases.GetAvailabilityAlias;
using Banisi.Application.Shared.Models.Base;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Utilitys.Presenters
{
    public class GetAvailabilityAliasPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ServiceResponse response)
        {
            SetOkObject(response);
        }
    }
}