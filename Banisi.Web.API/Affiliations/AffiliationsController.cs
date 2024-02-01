using Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase;
using Banisi.Application.AffiliationServices.UseCases.CreateAffiliationsUseCase;
using Banisi.Application.ModelsYappy;
using Banisi.Web.API.Affiliations.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace Banisi.Web.API.Affiliations
{
    [Route("api/affiliations")]
    [ApiController]
    public class AffiliationsController : ControllerBase
    {
        private readonly IGetCheckAffiliationUseCase _getCheckAffiliationUseCase;
        private readonly ICreateAffiliationUseCase _createAffiliationUseCase;

        public AffiliationsController(IGetCheckAffiliationUseCase getCheckAffiliationUseCase, ICreateAffiliationUseCase createAffiliationUseCase)
        {
            _getCheckAffiliationUseCase = getCheckAffiliationUseCase;
            _createAffiliationUseCase = createAffiliationUseCase;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> CheckAffiliation(int clientId)
        {
            var presenter = new GetCheckAffiliationPresenter();

            _getCheckAffiliationUseCase.SetOutputPort(presenter);
            await _getCheckAffiliationUseCase.Execute(clientId);

            return presenter.ActionResult;
        }

        [HttpPost("create-affiliated")]
        public async Task<IActionResult> Create(ClientAffiliationRequest model)
        {
            var presenter = new CreateAffiliationPresenter();

            _createAffiliationUseCase.SetOutputPort(presenter);
            await _createAffiliationUseCase.Execute(model);

            return presenter.ActionResult;
        }
    }
}