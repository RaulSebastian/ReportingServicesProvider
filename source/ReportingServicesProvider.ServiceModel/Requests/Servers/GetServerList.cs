using System.Collections.Generic;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers", "Get")]
    public class GetServerList : IReturn<List<ReportingServer>>
    {
    }
}