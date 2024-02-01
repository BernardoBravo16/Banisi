using Amazon.S3;
using Banisi.Application.ModelsYappy;
using Banisi.Application.Oauth2.UseCases.CreateOauthTokensUseCase;
using Banisi.Application.Oauth2.UseCases.GetOauthsAuthorizeUseCase;
using Banisi.Application.Utilitys.UseCases.GetTermsConditions;
using Banisi.Web.API.Oauth2.Presenters;
using Banisi.Web.API.Persons.Presenters;
using Banisi.Web.API.Utilitys.Presenters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Banisi.Web.API.Oauth2
{
    [Route("oauth2")]
    [ApiController]
    public class OauthAuthorizeController : ControllerBase
    {
        private readonly IGetOauthAuthorizeUseCase _getOauthAuthorizeUseCase;
        private readonly ICreateOauthTokenUseCase _createOauthTokenUseCase;

        public OauthAuthorizeController(IGetOauthAuthorizeUseCase getOauthAuthorizeUseCase, ICreateOauthTokenUseCase createOauthTokenUseCase)
        {
            _getOauthAuthorizeUseCase = getOauthAuthorizeUseCase;
            _createOauthTokenUseCase = createOauthTokenUseCase;
        }

        [HttpGet("authorize")]
        public async Task<IActionResult> GetOauthAutorize([FromQuery] OauthAuthorizeRequest request, [FromHeader , Required] string username, [FromHeader, Required] string password)
        {
            var presenter = new GetOauthAuthorizePresenter();

            _getOauthAuthorizeUseCase.SetOutputPort(presenter);
            await _getOauthAuthorizeUseCase.Execute(request, username, password);

            return presenter.ActionResult;
        }

        [HttpPost("token")]
        public async Task<IActionResult> PostPersonalProfile(Application.Oauth2.UseCases.CreateOauthTokensUseCase.OauthTokenRequest request)
        {
            var presenter = new CreateOauthTokenPresenter();

            _createOauthTokenUseCase.SetOutputPort(presenter);
            await _createOauthTokenUseCase.Execute(request);

            return presenter.ActionResult;
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListAsync()
        {
            var s3Client = new AmazonS3Client("AKIAVAPJHBZXVR4TK24F", "Ii0Ljx/xv+EeGL646fFTURNLjGfPPYsnVQLhYgwi");
            var data = await s3Client.ListBucketsAsync();
            var buckets = data.Buckets.Select(b => { return b.BucketName; });
            return Ok(buckets);
        }
    }
}