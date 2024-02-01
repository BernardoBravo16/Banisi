using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Common.Configuration;
using Microsoft.Extensions.Options;

namespace Banisi.Application.AvailabilityAlias.UseCases
{
    public class GetAvailabilityAliasUseCase : IGetAvailabilityAliasUseCase
    {
        private IOutputPort _outputPort;
        private IApiClientService _apiClientService;
        private readonly GeneralSettings _generalSettings;

        public GetAvailabilityAliasUseCase(IApiClientService apiClientService, IOptions<GeneralSettings> generalSettings)
        {
            _apiClientService = apiClientService;
            _generalSettings = generalSettings.Value;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(string alias, string appVersion, string clientIp)
        {
            string appName = _generalSettings.Yappy_AppName;

            var headers = new Dictionary<string, string>
            {
                { "Alias", alias },
                { "App-Version", appVersion },
                { "App-Name", appName },
                { "Client-Ip", clientIp }
            };

            var urlYappy = _generalSettings.Yappy_Url;

            string url = $"{urlYappy}utility/availability/alias";

            var response = await _apiClientService.GetAsync<ApiResponse<AvaliabilityAliasModel>>(url, headers);

            _outputPort.Ok(response);
        }
    }
}