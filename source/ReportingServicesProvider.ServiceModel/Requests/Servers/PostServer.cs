﻿using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;

namespace ReportingServicesProvider.ServiceModel.Requests.Servers
{
    [Route("/Servers", "Post")]
    public class PostServer : IReturn<Server>
    {
        [ApiMember(IsRequired = true)]
        public string Name { get; set; }

        [ApiMember(IsRequired = true)]
        public string Url { get; set; }

        [ApiMember(IsRequired = false)]
        public Platform? Platform { get; set; }
    }
}