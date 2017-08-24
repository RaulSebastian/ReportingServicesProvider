using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/{Name}", "Get")]
    public class GetServerByName : IReturn<ReportingServer>
    {
        [ApiMember(IsRequired = true)]
        public string Name { get; set; }
    }
}