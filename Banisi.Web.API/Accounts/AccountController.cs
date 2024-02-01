using Banisi.Application.Accounts.UseCases.GetAccountsByClientUseCase;
using Banisi.Application.ModelsYappy;
using Banisi.Web.API.Accounts.Presenters;
using Banisi.Web.API.Persons.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace Banisi.Web.API.Accounts
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGetAccountByClientUseCase _getAccountByClientUseCase;

        public AccountController(IGetAccountByClientUseCase getAccountByClientUseCase)
        {
            _getAccountByClientUseCase = getAccountByClientUseCase;
        }

        [HttpGet("account-by-client/{clientId}/{clientIp}")]
        public async Task<IActionResult> GetAccountByClient(int clientId, string clientIp)
        {
            var presenter = new GetAccountByClientPresenter();

            _getAccountByClientUseCase.SetOutputPort(presenter);
            await _getAccountByClientUseCase.Execute(clientId, clientIp);

            return presenter.ActionResult;
        }
    }
}