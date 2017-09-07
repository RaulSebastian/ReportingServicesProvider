using System;
using System.Net;
using ServiceStack;
using ServiceStack.Data;
using ReportingServicesProvider.ServiceModel.Requests.Reports;
using ReportingServicesProvider.ServiceInterface.Repositories;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ReportsService : Service
    {
        private readonly IReportsRepository _repository = HostContext.Container.Resolve<IReportsRepository>();

        public object Get(GetReportList request) => _repository.GetAll();

        public object Get(GetReportById request) => _repository.GetById(request.Id);

        public object Post(PostReport request) => _repository.Save(request.ToDto());

        public object Put(UpdateReportById request)
        {
            var report = request.ToDto();
            if (!_repository.Exists(report))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"ReportId {request.Id} does not exist.");
            }
            return _repository.Save(report);
        }

        public int Delete(DeleteReportById request)
        {
            var report = request.ToDto();
            if (!_repository.Exists(report))
                return 0;
            report.Name = $"{report.Name}_deleted_{DateTime.Now:yyyyMMddHHmmssmmm}";
            _repository.Save(report);
            return _repository.SetInactive(report);
        }
    }
}