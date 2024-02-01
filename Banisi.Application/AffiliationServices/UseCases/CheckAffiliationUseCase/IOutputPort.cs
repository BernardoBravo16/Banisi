using Banisi.Application.ModelsYappy;
using Banisi.Domain.Entities.Affiliations;

namespace Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase
{
    public interface IOutputPort
    {
        void Ok(ApiResponse result);
        void BadRequest(string message);
    }
}