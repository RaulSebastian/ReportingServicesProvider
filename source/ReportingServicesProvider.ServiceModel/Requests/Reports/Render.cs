using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports/{id}/Render", "Any")]
    [Route("/Servers/{sid}/Reports/{id}/Render", "Any")]
    public class Render : IReturn<Report>
    {
        [ApiMember(IsRequired = true)]
        public int SId { get; set; }
        
        [ApiMember(IsRequired = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true)]
        public string Path { get; set; }
    }
}