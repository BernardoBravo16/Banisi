using Banisi.Application.Oauth2.UseCases.GetOauthsAuthorizeUseCase;
using Banisi.Application.Shared.Models.Base;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Oauth2.Presenters
{
    public class GetOauthAuthorizePresenter : BasePresenter, IOutputPort
    {
        public void BadRequest(string message)
        {
            SetBadRequestWithMessage(message);
        }

        public void Ok(ServiceResponse response)
        {
            SetOkObject(response);
        }
    }
}