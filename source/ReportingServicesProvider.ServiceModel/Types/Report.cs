using System;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.ServiceModel.Types
{
    public class Report
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [Alias("ReportingServerId"), References(typeof(Server))]
        public int Server { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Path { get; set; }

        public DateTime Modified { get; set; }

        [Ignore]
        public DateTime Created { get; set; } = DateTime.Now;

        [IgnoreDataMember]
        public bool Active { get; set; } = true;
    }
}