namespace Banisi.Application.Otps.UseCases.GenerateOtpsUseCase
{
    public interface IGenerateOtpUseCase
    {
        Task Execute(OtpGeneratorModel model);
        void SetOutputPort(IOutputPort outputPort);
    }
}