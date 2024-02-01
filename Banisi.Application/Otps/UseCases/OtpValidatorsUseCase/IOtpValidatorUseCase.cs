namespace Banisi.Application.Otps.UseCases.OtpValidatorsUseCase
{
    public interface IOtpValidatorUseCase
    {
        Task Execute(OtpValidatorModel model);
        void SetOutputPort(IOutputPort outputPort);
    }
}