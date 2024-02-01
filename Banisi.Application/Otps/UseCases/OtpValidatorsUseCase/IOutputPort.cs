using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Otps.UseCases.OtpValidatorsUseCase
{
    public interface IOutputPort
    {
        public void Ok(ResponseOtp responseOtp);
    }
}