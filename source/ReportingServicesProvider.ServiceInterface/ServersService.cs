using System;
using System.Linq;
using System.Net;
using ReportingServicesProvider.ServiceInterface.Repositories;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ServersService : Service
    {
        private readonly IServersRepository _repository = HostContext.Container.Resolve<IServersRepository>();

        public object Get(GetServerList request) => _repository.GetAll();

        public object Get(GetServerNames request) => _repository.GetAll().Select(s => s.Name);

        public object Get(GetServerById request) => _repository.GetById(request.Id);

        public object Get(GetServerByName request) => _repository.GetByName(request.Name);

        public object Post(PostServer request)
        {
            var server = request.ToDto();
            if (_repository.Exists(server))
            {
                throw new HttpError(HttpStatusCode.Ambiguous, $"A server mamed '{request.Name}' already exists.");
            }
            return _repository.Save(server);
        }

        public object Put(UpdateServerById request)
        {
            var server = request.ToDto();
            if (_repository.Exists(server))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Server does not exist.");
            }
            return _repository.Save(server);
        }

        private int MarkAsDeleted(Server server)
        {
            if (!_repository.Exists(server))
                return 0;

            server.Name = $"{server.Name}_deleted_{DateTime.Now:yyyyMMddHHmmssmmm}";
            _repository.Save(server);
            return _repository.SetInactive(server);
        }

        public int Delete(DeleteServerById request) => MarkAsDeleted(request.ToDto());

        public int Delete(DeleteServerByName request) => MarkAsDeleted(request.ToDto());
    }
}