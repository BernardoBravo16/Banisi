using Banisi.Application.ModelsYappy;
using Banisi.Domain.Entities.Affiliations;

namespace Banisi.Application.AffiliationServices.UseCases.CreateAffiliationsUseCase
{
    public interface IOutputPort
    {
        void Ok(ApiResponse response);
        void BadRequest(string message);
    }
}