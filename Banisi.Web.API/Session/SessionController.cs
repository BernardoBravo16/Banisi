using Banisi.Application.ModelsYappy;
using Banisi.Application.Session.UseCases.SessionLoginUseCase;
using Banisi.Web.API.Session.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace Banisi.Web.API.Session
{
    [Route("api/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLoginUseCase _sessionLoginUseCase;

        public SessionController(ISessionLoginUseCase sessionLoginUseCase)
        {
            _sessionLoginUseCase = sessionLoginUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginSessionRequest request)
        {
            var presenter = new SessionLoginPresenter();

            _sessionLoginUseCase.SetOutputPort(presenter);
            await _sessionLoginUseCase.Execute(request);

            return presenter.ActionResult;
        }
    }
}