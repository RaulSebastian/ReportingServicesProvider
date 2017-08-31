using System;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.ServiceModel.Types
{
    public class Server
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Platform Platform { get; set; }

        [Required]
        public string Url { get; set; }

        public DateTime Modified { get; set; }
    }
}