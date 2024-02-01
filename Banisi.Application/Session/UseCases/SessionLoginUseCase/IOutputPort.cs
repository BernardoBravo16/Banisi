using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Session.UseCases.SessionLoginUseCase
{
    public interface IOutputPort
    {
        void Ok(ApiResponse response);
    }
}