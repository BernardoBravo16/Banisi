using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Persons.UseCases.CreatePersonsProfileUseCase
{
    public interface ICreatePersonProfileUseCase
    {
        Task Execute(string appVersion, string clientIp, ClientAffiliationRequest model);
        void SetOutputPort(IOutputPort outputPort);
    }
}