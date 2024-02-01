using Banisi.Application.ModelsYappy;
using Banisi.Domain.Entities.Affiliations;

namespace Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase
{
    public class AffiliationResult
    {
        public Affiliation Affiliation { get; set; }
        public Client AssociatedClient { get; set; }

        public AffiliationResult(Affiliation affiliation, Client associatedClient)
        {
            Affiliation = affiliation;
            AssociatedClient = associatedClient;
        }
    }
}