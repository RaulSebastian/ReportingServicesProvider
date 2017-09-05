using System.Linq;
using System.Collections.Generic;
using ReportingServicesProvider.Infrastructure.Model.Reporting;
using ServiceStack;
using ReportingServicesProvider.ServiceModel.Requests.Reports;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using Dtos = ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceInterface
{
    public static class Mappers
    {
        public static Dtos.Server ToDto(this Server from)
            => new Dtos.Server().PopulateWith(from);

        public static List<Dtos.Server> ToDto(this List<Server> from)
            => from.Map(x => x.ToDto()).ToList();

        public static Server ToModel(this PostServer from) 
            => new Server().PopulateWith(from);

        public static Server ToModel(this UpdateServerById from)
            => new Server().PopulateWith(from);

        public static Dtos.Report ToDto(this Report from)
            => new Dtos.Report().PopulateWith(from);

        public static List<Dtos.Report> ToDto(this List<Report> from)
            => from.Map(x => x.ToDto()).ToList();

        public static Report ToModel(this PostReport from)
            => new Report { Server = from.SId}.PopulateWith(from);

        public static Report ToModel(this UpdateReportById from)
            => new Report {Server = from.SId}.PopulateWith(from);
    }
}