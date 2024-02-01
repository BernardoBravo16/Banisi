using Banisi.Application.ModelsYappy;
using Banisi.Application.Shared.Contracts.Persistence;
using Banisi.Application.Shared.Resources;
using Banisi.Common.Configuration;
using Banisi.Domain.Entities.Affiliations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Banisi.Application.Customers.UseCases.CreateCustomerSeedUseCase
{
    public class CreateCustomerSeedUseCase : ICreateCustomerSeedUseCase
    {
        private readonly IRepository<Affiliation, Guid> _repository;
        private readonly GeneralSettings _generalSettings;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        public CreateCustomerSeedUseCase(IOptions<GeneralSettings> generalSettings, IRepository<Affiliation, Guid> repository, IUnitOfWork unitOfWork)
        {
            _generalSettings = generalSettings.Value;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public async Task Execute(CustomerSeedRequest model)
        {
            var id = Guid.NewGuid();

            var getAffiliation = await _repository.GetAll()
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (getAffiliation == null)
            {
                var errorResponse = new ApiResponse
                {
                    Data = null,
                    Status = new Status
                    {
                        Code = "400",
                        Description = VerificationsResources.ClientIsAffiliatedWithYappy
                    }
                };

                _outputPort.Ok(errorResponse);
                return;
            }

            getAffiliation.Seed = model.Seed;

            await _unitOfWork.SaveAsync();

            var successResponse = new ApiResponse
            {
                Data = null,
                Status = new Status
                {
                    Code = "200",
                    Description = "Transacción exitosa."
                }
            };

            _outputPort.Ok(successResponse);
            return;
        }
    }
}