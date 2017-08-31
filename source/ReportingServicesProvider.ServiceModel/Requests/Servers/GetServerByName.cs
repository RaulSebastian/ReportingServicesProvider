using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/Names/{name}", "Get")]
    public class GetServerByName : IReturn<Server>
    {
        [ApiMember(IsRequired = true)]
        public string Name { get; set; }
    }
}