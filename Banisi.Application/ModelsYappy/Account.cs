using System.Text.Json.Serialization;

namespace Banisi.Application.ModelsYappy
{
    public class Account
    {
        [JsonPropertyName("codigoCliente")]
        public int ClientCode { get; set; }

        [JsonPropertyName("categoriaSubAplicacion")]
        public string SubApplicationCategory { get; set; }

        [JsonPropertyName("codigoAplicacion")]
        public string ApplicationCode { get; set; }

        [JsonPropertyName("codigoAgencia")]
        public int AgencyCode { get; set; }

        [JsonPropertyName("codigoSubAplicacion")]
        public int SubApplicationCode { get; set; }

        [JsonPropertyName("numeroCuenta")]
        public long AccountNumber { get; set; }

        [JsonPropertyName("estado")]
        public string? State { get; set; }

        [JsonPropertyName("permiteDebito")]
        public bool AllowsDebit { get; set; }

        [JsonPropertyName("fechaApertura")]
        public string? OpeningDate { get; set; }

        [JsonPropertyName("fechaVencimiento")]
        public DateTime? ExpirationDate { get; set; }

        [JsonPropertyName("fechaProximoPago")]
        public DateTime? NextPaymentDate { get; set; }

        [JsonPropertyName("nombreCuenta")]
        public string? AccountName { get; set; }

        [JsonPropertyName("tasa")]
        public decimal Rate { get; set; }

        [JsonPropertyName("cuentaFormato")]
        public string FormattedAccount { get; set; }

        [JsonPropertyName("fecha")]
        public string? Date { get; set; }

        [JsonPropertyName("fechaAperturaComparacion")]
        public long OpeningDateComparison { get; set; }

        [JsonPropertyName("frecuenciaPagoInt")]
        public string PaymentFrequency { get; set; }

        [JsonPropertyName("nombreEjecutivo")]
        public string ExecutiveName { get; set; }

        [JsonPropertyName("nombreSubAplicacion")]
        public string SubApplicationName { get; set; }

        [JsonPropertyName("tarjetaDebito")]
        public int DebitCard { get; set; }

        // Agregando campos adicionales según el JSON proporcionado
        [JsonPropertyName("valor_1")]
        public decimal Value1 { get; set; }

        [JsonPropertyName("valor_2")]
        public decimal Value2 { get; set; }

        [JsonPropertyName("valor_3")]
        public decimal Value3 { get; set; }

        [JsonPropertyName("valor_4")]
        public decimal Value4 { get; set; }

        [JsonPropertyName("valor_5")]
        public decimal Value5 { get; set; }

        [JsonPropertyName("valor_6")]
        public decimal Value6 { get; set; }

        [JsonPropertyName("valor_7")]
        public decimal Value7 { get; set; }

        [JsonPropertyName("valor_8")]
        public decimal Value8 { get; set; }

        [JsonPropertyName("valor_9")]
        public decimal Value9 { get; set; }

        [JsonPropertyName("valor_10")]
        public decimal Value10 { get; set; }

        [JsonPropertyName("cuentaCompleta")]
        public string CompleteAccount { get; set; }
    }

    public class AdditionalInfo
    {
        // Assuming "infoAdicional" is a complex type, define its properties here.
        // If it is a simple type like a string or a number, just translate it directly.
    }

    public class ResultAccount
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("descripcion")]
        public string Description { get; set; } // Translated descripcion
    }

    public class RootObject
    {
        [JsonPropertyName("cuentas")]
        public List<Account> Accounts { get; set; }

        [JsonPropertyName("infoAdicional")]
        public AdditionalInfo? AdditionalInformation { get; set; } // Translated infoAdicional

        [JsonPropertyName("resultado")]
        public ResultAccount Result { get; set; }
    }
}