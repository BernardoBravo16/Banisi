namespace Banisi.Application.Otps.UseCases.GenerateOtpsUseCase
{
    public class OtpGeneratorModel
    {
        public int ClientId { get; set; }
        public string Phone { get; set; }
        public string ClientIp { get; set; }
    }
}