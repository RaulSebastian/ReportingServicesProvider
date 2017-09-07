using System;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.ServiceModel.Types
{
    [Alias("ReportingServer")]
    public class Server
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [Index(Unique = true, Clustered = false)]
        public string Name { get; set; }

        [Reference, Alias("ReportingPlatformId")]
        public Platform Platform { get; set; } = DefaultValues.DefaultReportingPlatform;

        [Required]
        public string Url { get; set; }

        public DateTime Modified { get; set; }

        [Ignore]
        public DateTime Created { get; set; } = DateTime.Now;

        [IgnoreDataMember]
        public bool Active { get; set; } = true;
    }
}