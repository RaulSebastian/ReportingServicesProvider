using System;
using System.Linq;
using System.Net;
using ReportingServicesProvider.ServiceInterface.Repositories;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;
using Platform = ReportingServicesProvider.ServiceModel.Types.Platform;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ServersService : Service
    {
        private readonly IServersRepository _repository = HostContext.Container.Resolve<IServersRepository>();

        public object Get(GetServerList request) => _repository.GetAll();

        public object Get(GetServerNames request) => _repository.GetAll().Select(s => s.Name).ToList();

        public object Get(GetServerById request) => _repository.GetById(request.Id);

        public object Get(GetServerByName request) => _repository.GetByName(request.Name);

        public object Post(PostServer request)
        {
            var server = request.ToDto();
            //move code to validation / exception handling
            if(request.Platform != null && !Enum.IsDefined(typeof(Platform),request.Platform))
            {
                throw new HttpError(HttpStatusCode.NotFound, ExceptionMessages.PlatformNotFound);
            }
            if (_repository.Exists(server))
            {
                throw new HttpError(HttpStatusCode.Ambiguous, ExceptionMessages.ServerNameAmbigous);
            }
            return _repository.Save(server);
        }

        public object Put(UpdateServerById request) => _repository.Save(request.ToDto());

        private int MarkAsDeleted(Server server)
        {
            if (!_repository.Exists(server))
                return 0;

            _repository.Rename(server, $"{server.Name}_deleted_{DateTime.Now:yyyyMMddHHmmssmmm}");
            return _repository.SetInactive(server);
        }

        public int Delete(DeleteServerById request) => MarkAsDeleted(request.ToDto());

        public int Delete(DeleteServerByName request) => MarkAsDeleted(request.ToDto());
    }
}