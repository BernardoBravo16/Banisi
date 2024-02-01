namespace Banisi.Application.Shared.Models.Base
{
    public class ServiceError
    {
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }

        public ServiceError(string message)
        {
            ErrorMessage = message;
            ErrorDetail = message;
        }
    }
}