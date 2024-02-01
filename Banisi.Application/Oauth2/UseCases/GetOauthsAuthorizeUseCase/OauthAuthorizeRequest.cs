using System.ComponentModel.DataAnnotations;

namespace Banisi.Application.Oauth2.UseCases.GetOauthsAuthorizeUseCase
{
    public class OauthAuthorizeRequest
    {
        [Required]
        public string Scope { get; set; }

        [Required]
        public string ResponseType { get; set; }

        [Required]
        public string ClientId { get; set; }
        public string CodeChallenge { get; set; }
        public string Otp { get; set; }
        public bool ShowScreen { get; set; }
        public string LoginHint { get; set; }

        [Required]
        public string RedirectUri { get; set; }
    }
}