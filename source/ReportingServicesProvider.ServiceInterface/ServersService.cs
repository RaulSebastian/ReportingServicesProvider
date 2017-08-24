using System.Collections.Generic;
using ServiceStack;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ServersService : Service
    {
        private readonly IDbConnectionFactory _dbFactory = HostContext.Container.Resolve<IDbConnectionFactory>();

        public object Get(GetServerList request)
        {
            using (var db = _dbFactory.Open())
            {
                return db.From<ReportingServer>().Where(rs => rs.Active);
            }
        }

        public object Get(GetServerById request)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<ReportingServer>(rs => rs.Id == request.Id && rs.Active);
            }
        }

        public object Get(GetServerByName request)
        {
            using (var db = _dbFactory.Open())
            {
                return db.Single<ReportingServer>(rs => rs.Name == request.Name && rs.Active);
            }
        }

        public object Post(PostServer request)
        {
            return new ReportingServer
            {
                Name = request.ServerName,
                ReportingPlatform = request.Platform ?? Defaults.DefaultReportingPlatform,
                Url = request.Url
            };
        }

        public object Put(UpdateServerById request)
        {
            return new ReportingServer
            {
                Name = request.ServerName,
                ReportingPlatform = request.Platform ?? Defaults.DefaultReportingPlatform,
                Url = request.Url
            };
        }

        public void Delete(DeleteServerById request)
        {

        }

        public void Delete(DeleteServerByName request)
        {

        }
    }
}