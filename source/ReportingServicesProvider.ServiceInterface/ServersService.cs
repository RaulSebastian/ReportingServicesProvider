using System.Linq;
using System.Net;
using ReportingServicesProvider.Infrastructure.Repositories;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using ServiceStack;
using ServiceStack.Data;

namespace ReportingServicesProvider.ServiceInterface
{
    public class ServersService : Service
    {
        private IServerRepository _serverRepository = new ServerRepository(HostContext.Container.Resolve<IDbConnectionFactory>());

        public void InjectRepository(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

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
            return _serverRepository.Create(request.ToModel()).ToDto();
        }

        public object Put(UpdateServerById request)
        {
            var existingServer = _serverRepository.Read(request.Id);
            if (existingServer == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"ServerId {request.Id} does not exist.");
            }
            return _serverRepository.Update(request.ToModel()).ToDto();
        }

        public int Delete(DeleteServerById request) => _serverRepository.Delete(request.Id);

        public int Delete(DeleteServerByName request) => _serverRepository.Delete(request.Name);
    }
}