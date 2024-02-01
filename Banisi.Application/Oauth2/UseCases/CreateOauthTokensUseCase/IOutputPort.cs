namespace Banisi.Application.Oauth2.UseCases.CreateOauthTokensUseCase
{
    public interface IOutputPort
    {
        void Ok(Shared.Models.Base.ServiceResponse response);
    }
}