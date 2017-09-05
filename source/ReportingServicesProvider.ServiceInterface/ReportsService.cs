using System.Net;
using System.Runtime.InteropServices;
using ReportingServicesProvider.Infrastructure.Model.Reporting;
using ReportingServicesProvider.Infrastructure.Repositories;
using ServiceStack;
using ServiceStack.Data;
using ReportingServicesProvider.ServiceModel.Requests.Reports;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ReportsService : Service
    {
        private readonly ReportRepository _reportRepository =
            new ReportRepository(HostContext.Container.Resolve<IDbConnectionFactory>());

        public object Get(GetReportList request) => _reportRepository.ReadAll().ToDto();

        public object Get(GetReportById request) => _reportRepository.Read(request.Id).ToDto();

        public object Post(PostReport request) => _reportRepository.Create(request.ToModel()).ToDto();

        public object Put(UpdateReportById request)
        {
            var existingReport = _reportRepository.Read(request.Id);
            if (existingReport == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"ReportId {request.Id} does not exist.");
            }
            return _reportRepository.Update(request.ToModel()).ToDto();
        }
        public int Delete(DeleteReportById request) => _reportRepository.Delete(request.Id);
    }
}