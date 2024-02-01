using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Session.UseCases.SessionLoginUseCase
{
    public interface ISessionLoginUseCase
    {
        Task Execute(LoginSessionRequest request);
        void SetOutputPort(IOutputPort outputPort);
    }
}