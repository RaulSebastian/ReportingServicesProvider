using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports", "Put")]
    [Route("/Reports/{id}", "Put")]
    public class UpdateReportById : IReturn<Server>
    {
        [ApiMember(IsRequired = true)]
        public int Id { get; set; }

        [ApiMember(IsRequired = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true)]
        public string Url { get; set; }

        [ApiMember(IsRequired = true)]
        public Platform Platform { get; set; }
    }
}