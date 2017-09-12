using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace ReportingServicesProvider.ServiceModel.Types
{
    [Alias("ReportingServer")]
    public class Server : IHasId<int>, IEquatable<Server>
    {
        [Required]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required]
        [StringLength(1,255)]
        [Index(Unique = true, Clustered = false)]
        public string Name { get; set; }

        [Reference, Alias("ReportingPlatformId")]
        public Platform Platform { get; set; } = DefaultValues.DefaultReportingPlatform;

        [Required]
        public string Url { get; set; }

        public DateTime Modified { get; set; } = DateTime.Now;

        [Ignore]
        public DateTime Created { get; set; }

        [IgnoreDataMember]
        public bool Active { get; set; } = true;

        public override bool Equals(object obj)
        {
            var to = (Server) obj;
            return to != null && Equals(to);
        }

        public bool Equals(Server to)
        {
            if (ReferenceEquals(null, to)) return false;
            if (ReferenceEquals(this, to)) return true;
            return (Name == to.Name || Id == to.Id)
                   && Platform == to.Platform
                   && Url == to.Url
                   && Active;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ StringComparer.CurrentCulture.GetHashCode(Name);
                hashCode = (hashCode * 397) ^ (int) Platform;
                hashCode = (hashCode * 397) ^ StringComparer.CurrentCulture.GetHashCode(Url);
                hashCode = (hashCode * 397) ^ Active.GetHashCode();
                return hashCode;
            }
        }
    }
}