using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports", "Get")]
    [Route("/Servers/{id}/Reports", "Get")]
    public class GetReportList : IReturn<List<Report>>
    {
        public int Id { get; set; }
    }
}