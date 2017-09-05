using System;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.ServiceModel.Types
{
    public class Report
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [References(typeof(Server))]
        public int Server { get; set; }

        [Required]
        public string Path { get; set; }

        public DateTime Modified { get; set; }
    }
}