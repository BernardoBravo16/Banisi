using Banisi.Application.ModelsYappy;
using Banisi.Web.API.Shared;
using Banisi.Application.Otps.UseCases.OtpValidatorsUseCase;

namespace Banisi.Web.API.Otps.Presenters
{
    public class OtpValidatorPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ResponseOtp responseOtp)
        {
            SetOkObject(responseOtp);
        }
    }
}