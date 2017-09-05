using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Reports
{
    [Route("/Reports", "Delete")]
    [Route("/Reports/{id}", "Delete")]
    [Route("/Servers/{sid}/Reports", "Delete")]
    [Route("/Servers/{sid}/Reports/{id}", "Delete")]
    public class DeleteReportById
    {
        [ApiMember(IsRequired = true)]
        public int Id { get; set; }

        [ApiMember(IsRequired = true)]
        public int Sid { get; set; }
    }
}