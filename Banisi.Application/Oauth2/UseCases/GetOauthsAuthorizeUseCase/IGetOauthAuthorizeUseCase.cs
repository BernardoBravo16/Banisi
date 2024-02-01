namespace Banisi.Application.Oauth2.UseCases.GetOauthsAuthorizeUseCase
{
    public interface IGetOauthAuthorizeUseCase
    {
        Task Execute(OauthAuthorizeRequest request, string username, string password);
        void SetOutputPort(IOutputPort outputPort);
    }
}