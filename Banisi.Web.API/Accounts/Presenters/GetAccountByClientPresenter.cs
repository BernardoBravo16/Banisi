using Banisi.Application.Accounts.UseCases.GetAccountsByClientUseCase;
using Banisi.Application.ModelsYappy;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Accounts.Presenters
{
    public class GetAccountByClientPresenter : BasePresenter, IOutputPort
    {
        public void Ok(RootObject rootObject)
        {
            SetOkObject(rootObject);
        }
    }
}