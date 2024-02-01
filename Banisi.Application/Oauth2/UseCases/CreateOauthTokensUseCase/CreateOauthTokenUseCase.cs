using Banisi.Application.Shared.Contracts.Infrastructure;

namespace Banisi.Application.Oauth2.UseCases.CreateOauthTokensUseCase
{
    public class CreateOauthTokenUseCase : ICreateOauthTokenUseCase
    {
        private IApiClientService _apiClientService;
        private IOutputPort _outputPort;

        public CreateOauthTokenUseCase(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(OauthTokenRequest request)
        {
            string url = "https://api-mobile-integration-qa.yappycloud.com/v1/oauth2/token";

            var response = await _apiClientService.PostAsync(url, request);

            _outputPort.Ok(response);
        }
    }
}