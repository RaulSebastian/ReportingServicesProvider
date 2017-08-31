using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ReportingServicesProvider.Logic.Model.Reporting;
using Dtos = ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceInterface
{
    public static class Mappers
    {
        public static Dtos.Server ToDto(this Server from)
            => new Dtos.Server().PopulateWith(from);

        public static List<Dtos.Server> ToDto(this List<Server> from)
            => from.Map(x => x.ToDto()).ToList();

        public static Dtos.Report ToDto(this Report from)
            => new Dtos.Report().PopulateWith(from);

        public static List<Dtos.Report> ToDto(this List<Report> from)
            => from.Map(x => x.ToDto()).ToList();

    }
}