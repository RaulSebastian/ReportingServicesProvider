using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports", "Post")]
    [Route("/Servers/Reports", "Post")]
    public class PostReport : IReturn<Report>
    {
        [ApiMember(IsRequired = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true)]
        public string Url { get; set; }

        [ApiMember(IsRequired = false)]
        public Platform? Platform { get; set; }
    }
}