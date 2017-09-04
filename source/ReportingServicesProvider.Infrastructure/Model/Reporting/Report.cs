using System;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.Infrastructure.Model.Reporting
{
    public class Report : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [References(typeof(Server)),
         Alias("ReportingServerId")]
        public int Server { get; set; }

        public DateTime Modified { get; set; } = DateTime.Now;

        [Ignore]
        public DateTime Created { get; set; } = DateTime.Now;

        [IgnoreDataMember]
        public bool Active { get; set; } = true;
    }
}