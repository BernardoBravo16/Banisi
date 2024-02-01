namespace Banisi.Application.AvailabilityAlias.UseCases
{
    public interface IOutputPort
    {
        void Ok(Shared.Models.Base.ServiceResponse response);
    }
}