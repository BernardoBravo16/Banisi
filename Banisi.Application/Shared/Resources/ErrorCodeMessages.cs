
namespace Banisi.Application.Shared.Resources
{
    public class ErrorCodeMessages
    {
        public static readonly Dictionary<string, string> ErrorMessages = new Dictionary<string, string>
    {
        {"YP-0000", "Se ha realizado la ejecución del servicio correctamente."},
        {"YP-0001", "Se ha realizado la ejecución del servicio correctamente, pero no se encontraron resultados."},
        {"YP-0002", "Errores relacionados con la búsqueda."},
        {"YP-0003", "Error, ha ocurrido un error en procesar los datos. Contacte al administrador"},
        {"YP-0004", "Error, uno o más campos obligatorios faltantes en el cuerpo de la petición"},
        {"YP-0005", "Error al procesar datos del perfil"},
        {"YP-0006", "Error, todas las solicitudes han fallado."},
        {"YP-0007", "Error al intentar cifrar los datos"},
        {"YP-0008", "Error, la cantidad de alias excede el máximo permitido"},
        {"YP-0009", "Error, el estado del deudor no es válido"},
        {"YP-0100", "Error, transacción duplicada"},
        {"YP-0101", "Error, ha ocurrido un error en procesar los datos. Contacte al administrador"},
        {"YP-0102", "Error, uno o más campos de la transacción no se validaron exitosamente"},
        {"YP-0200", "Error, el pedido ha expirado"},
        {"YP-0201", "Error, has sobrepasado el límite de pedidos diarios"},
        {"YP-0202", "Error, los datos del creador del pedido no coincide con los del deudor"},
        {"YP-0300", "Error, ha ocurrido un error en procesar los datos. Contacte al administrador"},
        {"YP-0301", "Error, ha ocurrido un error en procesar los datos. Contacte al administrador"},
        {"YP-0302", "Error, ha ocurrido un error en procesar los datos. Contacte al administrador"},
        {"YP-0303", "Error, ha ocurrido un error en procesar los datos. Contacte al administrador"},
        {"YP-9000", "Lo sentimos, actualmente estamos realizando labores de mantenimiento en nuestros servicios."},
        {"YP-9001", "Lo sentimos, actualmente estamos experimentando un degradamiento en nuestros servicios."},
        {"YP-9997", "Error, en estos momentos los servicios están indisponibles, intente de nuevo."},
        {"YP-9998", "Error, no se pudo ejecutar la solicitud por un fallo de infraestructura"},
        {"YP-9999", "Error, el servicio ha tardado en responder"}
    };

        public static string GetMessageForCode(string code)
        {
            if (ErrorMessages.TryGetValue(code, out var message))
            {
                return message;
            }
            return "Código de error desconocido.";
        }
    }
}
