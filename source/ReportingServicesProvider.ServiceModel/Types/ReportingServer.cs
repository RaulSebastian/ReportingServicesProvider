using System;
using System.Runtime.Serialization;

namespace ReportingServicesProvider.ServiceModel.Types
{
    public class ReportingServer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ReportingPlatform ReportingPlatform { get; set; }
        public string Url { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;

        [IgnoreDataMember]
        public DateTime Created { get; set; } = DateTime.Now;

        [IgnoreDataMember]
        public bool Active { get; set; }
    }
}