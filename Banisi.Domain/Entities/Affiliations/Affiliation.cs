using Banisi.Domain.Shared;

namespace Banisi.Domain.Entities.Affiliations
{ 
    /// <summary>
    ///Description: 
    /// Tabla Name:
    /// </summary>
    public class Affiliation : IGenericEntity<Guid>
    {
        /// Identificador de la tabla de afiliacion
        /// Column Name 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// identicador del cliente
        /// Column Name 
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// identicador de la cuenta
        /// Column Name 
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Codigo OTP generado automaticamente
        /// Column Name 
        /// </summary>
        public Guid Otp { get; set; }

        /// <summary>
        /// Codigo de semilla
        /// Column Name 
        /// </summary>
        public string? Seed { get; set; }

        /// <summary>
        /// Username de cognito
        /// Column Name 
        /// </summary>
        public string CognitoUsername { get; set; }

        /// <summary>
        /// Password de cognito
        /// Column Name 
        /// </summary>
        public string CognitoPassword { get; set; }

        /// <summary>
        /// Fecha de creacion de la afiliacion
        /// Column Name 
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}