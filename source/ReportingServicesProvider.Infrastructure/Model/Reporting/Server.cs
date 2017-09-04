using System;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.Infrastructure.Model.Reporting
{
    [Alias("ReportingServer")]
    public class Server : Entity
    {
        [Required]
        [Index(Unique = true, Clustered = false)]
        public string Name { get; set; }

        [Reference, Alias("ReportingPlatformId")]
        public Platform Platform { get; set; } = Defaults.DefaultReportingPlatform;

        [Required]
        public string Url { get; set; }

        public DateTime Modified { get; set; } = DateTime.Now;

        [Ignore]
        public DateTime Created { get; set; } = DateTime.Now;

        [IgnoreDataMember]
        public bool Active { get; set; } = true;
    }
}