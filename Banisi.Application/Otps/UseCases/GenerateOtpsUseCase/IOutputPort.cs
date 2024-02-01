using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Otps.UseCases.GenerateOtpsUseCase
{
    public interface IOutputPort
    {
        public void Ok(ResponseOtp responseOtp);
    }
}