using Banisi.Application.Oauth2.UseCases.CreateOauthTokensUseCase;
using Banisi.Application.Shared.Models.Base;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Oauth2.Presenters
{
    public class CreateOauthTokenPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ServiceResponse response)
        {
            SetOkObject(response);
        }
    }
}