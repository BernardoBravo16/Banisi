using Banisi.Application.ModelsYappy;
using Banisi.Application.Persons.UseCases.CreatePersonsProfileUseCase;
using Banisi.Application.Shared.Models.Base;
using Banisi.Web.API.Shared;

namespace Banisi.Web.API.Persons.Presenters
{
    public class CreatePersonProfilePresenter : BasePresenter, IOutputPort
    {
        public void Ok(StatusResponse response)
        {
            SetOkObject(response);
        }
    }
}