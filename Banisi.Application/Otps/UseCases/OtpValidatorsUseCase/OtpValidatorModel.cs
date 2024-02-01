namespace Banisi.Application.Otps.UseCases.OtpValidatorsUseCase
{
    public class OtpValidatorModel
    {
        public int ClientId { get; set; }
        public string Otp { get; set; }
        public string ClientIp { get; set; }
    }
}