using Banisi.Application.ModelsYappy;
using Banisi.Application.Otps.UseCases.GenerateOtpsUseCase;
using Banisi.Application.Otps.UseCases.OtpValidatorsUseCase;
using Banisi.Web.API.Otps.Presenters;
using Banisi.Web.API.Persons.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace Banisi.Web.API.Otps
{
    [Route("api/otp")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IGenerateOtpUseCase _generateOtpUseCase;
        private readonly IOtpValidatorUseCase _otpValidatorUseCase;

        public OtpController(IGenerateOtpUseCase generateOtpUseCase, IOtpValidatorUseCase otpValidatorUseCase)
        {
            _generateOtpUseCase = generateOtpUseCase;
            _otpValidatorUseCase = otpValidatorUseCase;
        }


        [HttpPost("otp-generator")]
        public async Task<IActionResult> GenerateOtp(OtpGeneratorModel model)
        {
            var presenter = new GenerateOtpPresenter();

            _generateOtpUseCase.SetOutputPort(presenter);
            await _generateOtpUseCase.Execute(model);

            return presenter.ActionResult;
        }

        [HttpPost("otp-validator")]
        public async Task<IActionResult> OtpValidator(OtpValidatorModel model)
        {
            var presenter = new OtpValidatorPresenter();

            _otpValidatorUseCase.SetOutputPort(presenter);
            await _otpValidatorUseCase.Execute(model);

            return presenter.ActionResult;
        }
    }
}