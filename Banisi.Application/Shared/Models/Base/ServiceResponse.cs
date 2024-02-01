using System.Net;

namespace Banisi.Application.Shared.Models.Base
{
    public class ServiceResponse : ServiceResponse<object>
    {
        public ServiceResponse() { }
        public ServiceResponse(object data) : base(data) { }
    }

    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Status => !Errors.Any();
        public HttpStatusCode StatusCode { get; set; }

        public ICollection<ServiceError> Errors { get; set; }

        public ServiceResponse()
        {
            Errors = new List<ServiceError>();
        }

        public ServiceResponse(T data)
        {
            Data = data;
        }

        public void SetFaultyState(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            AddError(errorMessage);
        }

        private void AddError(string message)
        {
            Errors.Add(new ServiceError(message));
        }
    }
}