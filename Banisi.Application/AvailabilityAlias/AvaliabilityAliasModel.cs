using Banisi.Domain.Shared;

namespace Banisi.Application.AvailabilityAlias
{
    public class AvaliabilityAliasModel : IGenericEntity<int>
    {
        public string Alias { get; set; }
        public string AppVersion { get; set; }
        public string AppName { get; set; }
        public string ClientIp { get; set; }
        public int Id { get; set; }
    }
}
