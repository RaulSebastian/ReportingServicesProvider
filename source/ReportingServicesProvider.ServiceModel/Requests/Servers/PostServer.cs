using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers", "Post")]
    public class PostServer : IReturn<ReportingServer>
    {
        [ApiMember(IsRequired = true)]
        public string ServerName { get; set; }

        [ApiMember(IsRequired = true)]
        public string Url { get; set; }

        [ApiMember(IsRequired = false)]
        public ReportingPlatform? Platform { get; set; }
    }
}