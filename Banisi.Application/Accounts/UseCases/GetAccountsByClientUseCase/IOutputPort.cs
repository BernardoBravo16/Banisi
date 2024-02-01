using Banisi.Application.ModelsYappy;

namespace Banisi.Application.Accounts.UseCases.GetAccountsByClientUseCase
{
    public interface IOutputPort
    {
        public void Ok(RootObject rootObject);
    }
}