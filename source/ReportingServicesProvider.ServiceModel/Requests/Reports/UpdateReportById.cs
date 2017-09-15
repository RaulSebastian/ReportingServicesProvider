using System.Runtime.Serialization;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports", "Put")]
    [Route("/Reports/{id}", "Put")]
    [Route("/Servers/{sid}/Reports", "Put")]
    [Route("/Servers/{sid}/Reports/{id}", "Put")]
    public class UpdateReportById : IReturn<Server>
    {
        [ApiMember(IsRequired = true)]
        public int Id { get; set; }

        [ApiMember(IsRequired = true)]
        public int Sid { get; set; }

        [ApiMember(IsRequired = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true)]
        public string Path { get; set; }
    }
}