using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Domain.Entities.Affiliations;
using Banisi.Application.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Banisi.Application.Oauth2.UseCases.GetOauthsAuthorizeUseCase
{
    public class GetOauthAuthorizeUseCase : IGetOauthAuthorizeUseCase
    {
        private readonly IRepository<Affiliation, Guid> _repository;
        private IApiClientService _apiClientService;
        private IOutputPort _outputPort;

        public GetOauthAuthorizeUseCase(IApiClientService apiClientService, IRepository<Affiliation, Guid> repository)
        {
            _apiClientService = apiClientService;
            _repository = repository;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(OauthAuthorizeRequest request, string username, string password)
        {
            Guid usernameGuid;
            bool isUsernameGuid = Guid.TryParse(username, out usernameGuid);

            Guid passwordGuid;
            bool isPasswordGuid = Guid.TryParse(password, out passwordGuid);

            if (!isUsernameGuid || !isPasswordGuid)
            {
                _outputPort.BadRequest(VerificationsResources.IncorrectFormatGuid);
                return;
            }

            var affiliation = await _repository.GetAll()
                .Where(a => a.Id == usernameGuid)
                .Where(a => a.Otp == passwordGuid)
                .FirstOrDefaultAsync();

            if (affiliation == null)
            {
                _outputPort.BadRequest(VerificationsResources.InvalidUser);
                return;
            }

            var queryParams = new Dictionary<string, string>
            {
                { "scope", request.Scope },
                { "response_type", request.ResponseType },
                { "client_id", request.ClientId },
                { "code_challenge", request.CodeChallenge },
                { "otp", request.Otp },
                { "show_screen", request.ShowScreen.ToString().ToLower() },
                { "login_hint", request.LoginHint },
                { "redirect_uri", request.RedirectUri }
            };

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            foreach (var param in queryParams)
            {
                queryString[param.Key] = param.Value;
            }

            string url = "https://api-mobile-integration-qa.yappycloud.com/v1/oauth2/authorize?" + queryString;

            var response = await _apiClientService.GetAsync<OauthAuthorizeRequest>(url);

            _outputPort.Ok(response);
        }
    }
}