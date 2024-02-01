using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Banisi.Application.ModelsYappy
{
    public class Client
    {
        [JsonPropertyName("codigoCliente")]
        public int ClientCode { get; set; } // Translated CodigoCliente

        [JsonPropertyName("nombreCompleto")]
        public string FullName { get; set; } // Translated NombreCompleto

        [JsonPropertyName("identificacion")]
        public string Identification { get; set; } // Translated Identificacion

        [JsonPropertyName("codigoTipoIdentificacion")]
        public int IdentificationTypeCode { get; set; } // Translated CodigoTipoIdentificacion

        [JsonPropertyName("tipoIdentificacion")]
        public string IdentificationType { get; set; } // Translated TipoIdentificacion

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("emailAnterior")]
        public string? PreviousEmail { get; set; } // Translated EmailAnterior

        [JsonPropertyName("estadoCivil")]
        public string MaritalStatus { get; set; } // Translated EstadoCivil

        [JsonPropertyName("codigoEstadoCivil")]
        public string MaritalStatusCode { get; set; } // Translated CodigoEstadoCivil

        [JsonPropertyName("esPEPS")]
        public string IsPEPS { get; set; } // Translated EsPEPS

        [JsonPropertyName("cambioIdentificacion")]
        public string? IdentificationChange { get; set; } // Translated CambioIdentificacion

        [JsonPropertyName("actualizacionPendiente")]
        public int PendingUpdate { get; set; } // Translated ActualizacionPendiente

        [JsonPropertyName("fechaNacimiento")]
        public DateTime? BirthDate { get; set; } // Translated FechaNacimiento

        [JsonPropertyName("fechaActualizacion")]
        public DateTime? UpdateDate { get; set; } // Translated FechaActualizacion

        [JsonPropertyName("numeroTelefono")]
        public string PhoneNumber { get; set; } // Translated NumeroTelefono

        [JsonPropertyName("paisNacimiento")]
        public string PlaceBirth { get; set; } // Translated NumeroTelefono

        [JsonPropertyName("resultado")]
        public Result Result { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("descripcion")]
        public string Description { get; set; }
    }
}
