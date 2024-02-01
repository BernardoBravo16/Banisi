using Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase;
using Banisi.Web.API.Shared;
using Banisi.Application.ModelsYappy;

namespace Banisi.Web.API.Affiliations.Presenters
{
    public class GetCheckAffiliationPresenter : BasePresenter, IOutputPort
    {
        public void BadRequest(string message)
        {
            SetBadRequestWithMessage(message);
        }

        public void Ok(ApiResponse result)
        {
            SetOkObject(result);
        }
    }
}
