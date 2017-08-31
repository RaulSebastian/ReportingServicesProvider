using ServiceStack;
using ServiceStack.Data;
using ReportingServicesProvider.Logic.Repositories;
using ReportingServicesProvider.Logic.Model.Reporting;
using ReportingServicesProvider.ServiceModel.Requests.Reports;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ReportsService : Service
    {
        private readonly ReportRepository _reportRepository =
            new ReportRepository(HostContext.Container.Resolve<IDbConnectionFactory>());

        public object Get(GetReportList request) => _reportRepository.ReadAll();

        public object Get(GetReportById request) => _reportRepository.Read(request.Id).ToDto();

        public object Post(PostReport request) => _reportRepository.Create(new Report().PopulateWith(request)).ToDto();

        public object Put(UpdateReportById request) => _reportRepository.Update
    }
}