using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/{id}", "Delete")]
    public class DeleteServerById
    {
        [ApiMember(IsRequired = true)]
        public int Id { get; set; }
    }
}