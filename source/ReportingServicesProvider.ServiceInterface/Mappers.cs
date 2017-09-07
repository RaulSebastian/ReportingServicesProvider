using ServiceStack;
using ReportingServicesProvider.ServiceModel.Requests.Reports;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceInterface
{
    public static class Mappers
    {
        public static Server ToDto(this PostServer from)
            => new Server().PopulateWith(from);

        public static Server ToDto(this UpdateServerById from)
            => new Server().PopulateWith(from);

        public static Server ToDto(this DeleteServerById from)
            => new Server().PopulateWith(from);

        public static Server ToDto(this DeleteServerByName from)
            => new Server().PopulateWith(from);

        public static Report ToDto(this PostReport from)
            => new Report {Server = from.SId}.PopulateWith(from);

        public static Report ToDto(this UpdateReportById from)
            => new Report {Server = from.SId}.PopulateWith(from);
        
        public static Report ToDto(this DeleteReportById from)
            => new Report {Server = from.SId}.PopulateWith(from);
        
    }
}