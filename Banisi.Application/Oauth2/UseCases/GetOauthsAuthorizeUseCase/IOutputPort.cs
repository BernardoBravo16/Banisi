namespace Banisi.Application.Oauth2.UseCases.GetOauthsAuthorizeUseCase
{
    public interface IOutputPort
    {
        void Ok(Shared.Models.Base.ServiceResponse response);
        void BadRequest(string message);
    }
}