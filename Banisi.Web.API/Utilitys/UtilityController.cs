using Banisi.Application.Utilitys;
using Microsoft.AspNetCore.Mvc;
using Banisi.Web.API.Affiliations.Presenters;
using Banisi.Web.API.Utilitys.Presenters;
using Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase;
using Banisi.Application.Utilitys.UseCases.GetAvailabilityAlias;
using Banisi.Application.Utilitys.UseCases.GetTermsConditions;

namespace Banisi.Web.API.Utilitys
{
    [Route("api/utility")]
    [ApiController]
    public class UtilityController: ControllerBase
    {
        private readonly IGetAvailabilityAliasUseCase _getAvailabilityAliasUseCase;
        private readonly IGetTermsConditiosUseCase _getTermsConditiosUsecase;

        public UtilityController(IGetAvailabilityAliasUseCase getAvailabilityAliasUseCase, IGetTermsConditiosUseCase getTermsConditiosUsecase)
        {
            _getAvailabilityAliasUseCase = getAvailabilityAliasUseCase;
            _getTermsConditiosUsecase = getTermsConditiosUsecase;
        }

        [HttpGet("availability/alias/{alias}/{appVersion}/{clientIp}")]
        public async Task<IActionResult> GetAvailabilityAlias(string alias, string appVersion, string clientIp)
        {
            var presenter = new GetAvailabilityAliasPresenter();

            _getAvailabilityAliasUseCase.SetOutputPort(presenter);
            await _getAvailabilityAliasUseCase.Execute(alias, appVersion, clientIp);

            return presenter.ActionResult;
        }

        [HttpGet("terms-conditions/{appVersion}/{clientIp}")]
        public async Task<IActionResult> GetTermsConditions(string appVersion, string clientIp)
        {
            var presenter = new GetTermsConditiosPresenter();

            _getTermsConditiosUsecase.SetOutputPort(presenter);
            await _getTermsConditiosUsecase.Execute(appVersion, clientIp);

            return presenter.ActionResult;
        }
    }
}
