using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers", "Put")]
    [Route("/Servers/{id}", "Put")]
    public class UpdateServerById : IReturn<Server>
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