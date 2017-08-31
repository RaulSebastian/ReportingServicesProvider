using System;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.Logic.Model.Reporting
{
    public class Report : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Reference, Alias("ReportingServerId")]
        public Server Server { get; set; }
        
        public DateTime Modified { get; set; }

        [Ignore]
        public DateTime Created { get; set; } = DateTime.Now;

        [IgnoreDataMember]
        public bool Active { get; set; } = true;
    }
}