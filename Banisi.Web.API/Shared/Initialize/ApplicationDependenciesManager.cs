using Banisi.Application.AffiliationServices.UseCases.CheckAffiliationUseCase;
using Banisi.Application.Utilitys.UseCases.GetAvailabilityAlias;
using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Infrastructure.Services;
using Banisi.Application.Utilitys.UseCases.GetTermsConditions;
using Banisi.Application.AffiliationServices.UseCases.CreateAffiliationsUseCase;
using Banisi.Application.Persons.UseCases.CreatePersonsProfileUseCase;
using Banisi.Application.Oauth2.UseCases.GetOauthsAuthorizeUseCase;
using Banisi.Application.Oauth2.UseCases.CreateOauthTokensUseCase;
using Banisi.Application.Accounts.UseCases.GetAccountsByClientUseCase;
using Banisi.Application.Otps.UseCases.GenerateOtpsUseCase;
using Banisi.Application.Otps.UseCases.OtpValidatorsUseCase;
using Banisi.Application.Customers.UseCases.CreateCustomerSeedUseCase;
using Banisi.Application.Customers.UseCases.GetCustomerAccountsUseCase;
using Banisi.Application.Customers.UseCases.GetCustomerInformationUseCase;
using Banisi.Application.Session.UseCases.SessionLoginUseCase;

namespace Banisi.Web.API.Shared.Initialize
{
    public static class ApplicationDependenciesManager
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Affiliation
            services.AddTransient<IGetCheckAffiliationUseCase, GetCheckAffiliationUseCase>();
            services.AddTransient<ICreateAffiliationUseCase, CreateAffiliationUseCase>();

            //Utility
            services.AddTransient<IGetTermsConditiosUseCase, GetTermsConditiosUseCase>();
            services.AddTransient<IGetAvailabilityAliasUseCase, GetAvailabilityAliasUseCase>();

            //Personal
            services.AddTransient<ICreatePersonProfileUseCase, CreatePersonProfileUseCase>();

            //Oauth
            services.AddTransient<IGetOauthAuthorizeUseCase, GetOauthAuthorizeUseCase>();
            services.AddTransient<ICreateOauthTokenUseCase, CreateOauthTokenUseCase>();

            //Accounts
            services.AddTransient<IGetAccountByClientUseCase, GetAccountByClientUseCase>();

            //OTP
            services.AddTransient<IGenerateOtpUseCase, GenerateOtpUseCase>();
            services.AddTransient<IOtpValidatorUseCase, OtpValidatorUseCase>();

            //Customers

            services.AddTransient<ICreateCustomerSeedUseCase, CreateCustomerSeedUseCase>();
            services.AddTransient<IGetCustomerAccountsUseCase, GetCustomerAccountsUseCase>();
            services.AddTransient<IGetCustomerInformationUseCase, GetCustomerInformationUseCase>();

            //Session
            services.AddTransient<ISessionLoginUseCase, SessionLoginUseCase>();
            
            
            services.AddTransient<IApiClientService, ApiClientService>();


            return services;
        }
    }
}