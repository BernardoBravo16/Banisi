using Banisi.Application.AffiliationServices.UseCases.CreateAffiliationsUseCase;
using Banisi.Application.ModelsYappy;
using Banisi.Domain.Entities.Affiliations;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Affiliations.Presenters
{
    public class CreateAffiliationPresenter : BasePresenter, IOutputPort
    {
        public void BadRequest(string message)
        {
            SetBadRequestWithMessage(message);
        }

        public void Ok(ApiResponse response)
        {
            SetOkObject(response);
        }
    }
}