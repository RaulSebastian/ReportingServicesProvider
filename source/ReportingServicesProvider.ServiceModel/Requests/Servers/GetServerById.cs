using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/{id}", "Get")]
    public class GetServerById : IReturn<Server>
    {
        [ApiMember(IsRequired = true)]
        public int Id { get; set; }
    }
}