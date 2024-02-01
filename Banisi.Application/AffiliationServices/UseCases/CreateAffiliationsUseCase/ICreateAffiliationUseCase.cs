using Banisi.Application.ModelsYappy;

namespace Banisi.Application.AffiliationServices.UseCases.CreateAffiliationsUseCase
{
    public interface ICreateAffiliationUseCase
    {
        Task Execute(ClientAffiliationRequest model);
        void SetOutputPort(IOutputPort outputPort);
    }
}