using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports/{id}", "Get")]
    [Route("/Servers/{sid}/Reports/{id}", "Get")]
    public class GetReportById : IReturn<Report>
    {
        public int SId { get; set; }

        [ApiMember(IsRequired = true)]
        public int Id { get; set; }
    }
}