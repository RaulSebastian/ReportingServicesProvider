using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/{Id}", "Get")]
    public class GetServerById : IReturn<ReportingServer>
    {
        [ApiMember(IsRequired = true)]
        public int Id { get; set; }
    }
}