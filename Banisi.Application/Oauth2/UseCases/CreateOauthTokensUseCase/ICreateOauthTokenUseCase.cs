namespace Banisi.Application.Oauth2.UseCases.CreateOauthTokensUseCase
{
    public interface ICreateOauthTokenUseCase
    {
        Task Execute(OauthTokenRequest request);
        void SetOutputPort(IOutputPort outputPort);
    }
}