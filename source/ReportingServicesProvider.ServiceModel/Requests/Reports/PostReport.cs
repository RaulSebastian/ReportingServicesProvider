using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports", "Post")]
    [Route("/Servers/{sid}/Reports", "Post")]
    public class PostReport : IReturn<Report>
    {
        [ApiMember(IsRequired = true)]
        public int Sid { get; set; }
        
        [ApiMember(IsRequired = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true)]
        public string Path { get; set; }
    }
}