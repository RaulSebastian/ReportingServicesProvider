using System.Collections.Generic;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers/Names", "Get")]
    public class GetServerNames : IReturn<List<string>> //TODO: consider replacing with a response dto for a clearer swagger model
    {
    }
}