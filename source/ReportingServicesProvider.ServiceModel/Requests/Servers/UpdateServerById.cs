using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/{Id}", "Put")]
    public class UpdateServerById : IReturn<ReportingServer>
    {
        //TODO: check client serialization for specific DTO definition
        [ApiMember(IsRequired = true)]
        public int Id { get; set; }

        public string ServerName { get; set; }
        public string Url { get; set; }
        public ReportingPlatform? Platform { get; set; }
    }
}