using Banisi.Application.ModelsYappy;
using Banisi.Application.Persons.UseCases.CreatePersonsProfileUseCase;
using Banisi.Application.Utilitys.UseCases.GetTermsConditions;
using Banisi.Web.API.Utilitys.Presenters;
using Banisi.Web.API.Persons.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace Banisi.Web.API.Persons
{
    [Route("api/personal")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly ICreatePersonProfileUseCase _createPersonProfileUseCase;

        public PersonalController(ICreatePersonProfileUseCase createPersonProfileUseCase)
        {
            _createPersonProfileUseCase = createPersonProfileUseCase;
        }

        [HttpPost("profile/{appVersion}/{clientIp}")]
        public async Task<IActionResult> PostPersonalProfile(string appVersion, string clientIp, ClientAffiliationRequest model)
        {
            var presenter = new CreatePersonProfilePresenter();

            _createPersonProfileUseCase.SetOutputPort(presenter);
            await _createPersonProfileUseCase.Execute(appVersion, clientIp, model);

            return presenter.ActionResult;
        }
    }
}
