using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/{Name}", "Delete")]
    public class DeleteServerByName
    {
        [ApiMember(IsRequired = true)]
        public string Name { get; set; }
    }
}