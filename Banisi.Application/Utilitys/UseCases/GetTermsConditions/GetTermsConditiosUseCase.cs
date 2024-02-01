using Banisi.Application.Utilitys;
using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Common.Configuration;
using Microsoft.Extensions.Options;

namespace Banisi.Application.Utilitys.UseCases.GetTermsConditions
{
    public class GetTermsConditiosUseCase : IGetTermsConditiosUseCase
    {
        private IApiClientService _apiClientService;
        private IOutputPort _outputPort;
        private readonly GeneralSettings _generalSettings;

        public GetTermsConditiosUseCase(IApiClientService apiClientService, IOptions<GeneralSettings> generalSettings)
        {
            _apiClientService = apiClientService;
            _generalSettings = generalSettings.Value;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(string appVersion, string clientIp)
        {
            string appName = _generalSettings.Yappy_AppName;

            var headers = new Dictionary<string, string>
            {
                { "App-Version", appVersion },
                { "App-Name", appName },
                { "Client-Ip", clientIp },
                { "App-Api-Key", _generalSettings.Yappy_Api_Key },
                { "App-Secret-Key", _generalSettings.Yappy_Secrect_Key }
            };

            var urlYappy = _generalSettings.Yappy_Url;

            string url = $"{urlYappy}utility/terms-conditions";

            var response = await _apiClientService.GetAsync<TermsConditionsResponse>(url, headers);

            _outputPort.Ok(response);
        }
    }
}
