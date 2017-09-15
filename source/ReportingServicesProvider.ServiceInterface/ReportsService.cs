using System.Net;
using ServiceStack;
using ReportingServicesProvider.ServiceModel.Requests.Reports;
using ReportingServicesProvider.ServiceInterface.Repositories;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ReportsService : Service
    {
        private readonly IReportsRepository _repository = HostContext.Container.Resolve<IReportsRepository>();

        public object Get(GetReportList request) => _repository.GetAllByServerId(request.Sid);

        public object Get(GetReportById request)
        {
            var report = _repository.GetById(request.Id);
            return report?.Server != request.Sid ? null : report;
        }

        public object Post(PostReport request)
        {
            var report = request.ToDto();
            if (!_repository.ServerExists(report))
                throw new HttpError(HttpStatusCode.NotFound, ExceptionMessages.ServerNotFound);
            return _repository.Save(report);
        }

        public object Put(UpdateReportById request) => _repository.Save(request.ToDto());

        public int Delete(DeleteReportById request)
        {
            var report = request.ToDto();
            return !_repository.Exists(report) ? 0 : _repository.SetInactive(report);
        }
    }
}