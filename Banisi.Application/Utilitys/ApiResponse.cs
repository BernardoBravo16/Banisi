using Banisi.Application.Shared.Resources;

namespace Banisi.Application.Utilitys
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }

        public ApiResponse()
        {
            IsSuccess = true; // Suponemos que la respuesta es exitosa por defecto
        }

        // Método para asignar un error a la respuesta
        public void SetError(string errorCode)
        {
            ErrorCode = errorCode;
            ErrorMessage = ErrorCodeMessages.ErrorMessages.ContainsKey(errorCode)
                ? ErrorCodeMessages.ErrorMessages[errorCode]
                : "Código de error desconocido";
            IsSuccess = false;
        }
    }
}
