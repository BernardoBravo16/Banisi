namespace Banisi.Common.Configuration
{
    public class GeneralSettings
    {
        public const string SectionName = "General";

        /// <summary>
        /// Usuario para consumir endpoints de banisi
        /// </summary>
        public string Banisi_UserName { get; set; }

        /// <summary>
        /// Password para consumir endpoints de banisi
        /// </summary>
        public string Banisi_Password { get; set; }

        /// <summary>
        /// La url de los servicios de Banisi
        /// </summary>
        public string Banisi_Url { get; set; }

        /// <summary>
        /// Access key de la cuenta de AWS
        /// </summary>
        public string AWS_AccessKey { get; set; }

        /// <summary>
        /// Secrect key de la cuenta de AWS
        /// </summary>
        public string AWS_SecretKey { get; set; }

        /// <summary>
        /// identifica la region de la cuenta de AWS
        /// </summary>
        public string AWS_Region { get; set; }

        /// <summary>
        /// Codigo de client de AWS
        /// </summary>
        public string AWS_ClientId { get; set; }

        /// <summary>
        /// Codigo del Pool de aplicacion de AWS
        /// </summary>
        public string AWS_UserPool { get; set; }

        /// <summary>
        /// Nombre del aplicativo de Yappy
        /// </summary>
        public string Yappy_AppName { get; set; }

        /// <summary>
        /// Url para consumir servicios de Yappy
        /// </summary>
        public string Yappy_Url { get; set; }

        /// <summary>
        /// Access token de Yappy
        /// </summary>
        public string Yappy_Access_Token { get; set; }

        /// <summary>
        /// Api Key de Yappy
        /// </summary>
        public string Yappy_Api_Key { get; set; }

        /// <summary>
        /// Secrcet Key de Yappy 
        /// </summary>
        public string Yappy_Secrect_Key { get; set; }
    }
}