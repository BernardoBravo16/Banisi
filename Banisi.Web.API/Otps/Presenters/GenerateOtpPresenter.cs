using Banisi.Application.ModelsYappy;
using Banisi.Application.Otps.UseCases.GenerateOtpsUseCase;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Otps.Presenters
{
    public class GenerateOtpPresenter : BasePresenter, IOutputPort
    {
        public void Ok(ResponseOtp responseOtp)
        {
            SetOkObject(responseOtp);
        }
    }
}