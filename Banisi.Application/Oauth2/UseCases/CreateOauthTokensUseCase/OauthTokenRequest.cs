namespace Banisi.Application.Oauth2.UseCases.CreateOauthTokensUseCase
{
    public class OauthTokenRequest
    {
        public string Code { get; set; }
        public string GrantType { get; set; } = "authorization_code";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; } = "openid";
    }
}