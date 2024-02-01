using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Persons.UseCases.CreatePersonsProfileUseCase
{
    public interface IOutputPort
    {
        void Ok(StatusResponse statusResponse);
    }
}