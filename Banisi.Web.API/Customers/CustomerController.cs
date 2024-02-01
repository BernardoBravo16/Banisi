using Banisi.Application.Customers.UseCases.CreateCustomerSeedUseCase;
using Banisi.Application.Customers.UseCases.GetCustomerAccountsUseCase;
using Banisi.Application.Customers.UseCases.GetCustomerInformationUseCase;
using Banisi.Application.ModelsYappy;
using Banisi.Web.API.Customers.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace Banisi.Web.API.Customers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICreateCustomerSeedUseCase _createCustomerSeedUseCase;
        private readonly IGetCustomerAccountsUseCase _getCustomerAccountsUseCase;
        private readonly IGetCustomerInformationUseCase _getCustomerInformationUseCase;

        public CustomerController(ICreateCustomerSeedUseCase createCustomerSeedUseCase, IGetCustomerAccountsUseCase getCustomerAccountsUseCase, IGetCustomerInformationUseCase getCustomerInformationUseCase)
        {
            _createCustomerSeedUseCase = createCustomerSeedUseCase;
            _getCustomerAccountsUseCase = getCustomerAccountsUseCase;
            _getCustomerInformationUseCase = getCustomerInformationUseCase;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Create(
            CustomerSeedRequest model, [FromHeader] string ApiKey, [FromHeader] string ClientSecret,
            [FromHeader] string TokenId)
        {
            var presenter = new CreateCustomerSeedPresenter();

            _createCustomerSeedUseCase.SetOutputPort(presenter);
            await _createCustomerSeedUseCase.Execute(model);

            return presenter.ActionResult;
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetCustomerAccount()
        {
            var presenter = new GetCustomerAccountsPresenter();

            _getCustomerAccountsUseCase.SetOutputPort(presenter);
            await _getCustomerAccountsUseCase.Execute();

            return presenter.ActionResult;
        }

        [HttpGet("information")]
        public async Task<IActionResult> GetCustomerInformation()
        {
            var presenter = new GetCustomerInformationPresenter();

            _getCustomerInformationUseCase.SetOutputPort(presenter);
            await _getCustomerInformationUseCase.Execute();

            return presenter.ActionResult;
        }
    }
}
