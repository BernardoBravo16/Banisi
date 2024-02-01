using Banisi.Application.ModelsYappy;
using Banisi.Web.API.Shared;
using Banisi.Application.Session.UseCases.SessionLoginUseCase;

namespace Banisi.Web.API.Session.Presenters
{
    public class SessionLoginPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ApiResponse response)
        {
            SetOkObject(response);
        }
    }
}