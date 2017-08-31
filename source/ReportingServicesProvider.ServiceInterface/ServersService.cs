using System;
using System.Data;
using System.Linq;
using System.Net;
using ReportingServicesProvider.Logic.Model.Reporting;
using ReportingServicesProvider.Logic.Repositories;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using Server = ReportingServicesProvider.Logic.Model.Reporting.Server;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ServersService : Service
    {
        private readonly ServerRepository _serverRepository =
            new ServerRepository(HostContext.Container.Resolve<IDbConnectionFactory>());

        public object Get(GetServerList request) => _serverRepository.ReadAll().ToDto();

        public object Get(GetServerNames request) => _serverRepository.ReadAll().Select(r => r.Name);

        public object Get(GetServerById request) => _serverRepository.Read(request.Id).ToDto();

        public object Get(GetServerByName request) => _serverRepository.ReadByName(request.Name).ToDto();

        public object Post(PostServer request)
        {
            var existingServer = _serverRepository.ReadByName(request.Name);
            if (existingServer != null)
            {
                throw new HttpError(HttpStatusCode.Conflict, $"A server mamed '{request.Name}' already exists.");
            }
            return _serverRepository.Create(new Server().PopulateWith(request)).ToDto();
        }

        public object Put(UpdateServerById request)
        {
            var existingServer = _serverRepository.Read(request.Id);
            if (existingServer == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"ServerId {request.Id} does not exist.");
            }
            return _serverRepository.Update(new Server().PopulateWith(request));
        }

        public int Delete(DeleteServerById request) => _serverRepository.Delete(request.Id);

        public int Delete(DeleteServerByName request) => _serverRepository.Delete(request.Name);

    }
}