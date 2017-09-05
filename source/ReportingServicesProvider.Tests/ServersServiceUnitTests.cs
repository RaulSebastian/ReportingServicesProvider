using System;
using System.Collections.Generic;
using System.Linq;
using ReportingServicesProvider.Infrastructure.Model.Reporting;
using ReportingServicesProvider.Infrastructure.Repositories;
using ServiceStack;
using ServiceStack.Testing;
using ReportingServicesProvider.ServiceInterface;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using Xunit;

namespace ReportingServicesProvider.Tests
{
    [Trait("Category","Unit")]
    public class ServersServiceUnitTests : IDisposable
    {
        private readonly ServiceStackHost _appHost;

        public ServersServiceUnitTests()
        {
            _appHost = new BasicAppHost(typeof(ServersService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //container.Register<IDbConnectionFactory>(c =>
                    //    new OrmLiteConnectionFactory(":memory:", SqlliteDialect.Provider));
                }
            }
            .Init();
        }

        public void Dispose()
        {
            _appHost.Dispose();
        }

        [Fact]
        public void Test_Method1()
        {
            var service = _appHost.Container.Resolve<ServersService>();
            //service.InjectRepository(new );

            //var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            var response = service.Get(new GetServerList());

            //Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }

        private class MockedServiceRepository : IServerRepository
        {
            private readonly List<Server> _servers = new List<Server>();

            public Server Create(Server server)
            {
                var id = _servers.Count + 1;
                server.Id = id;
                _servers.AddIfNotExists(server);

                return server;
            }

            public int Delete(int id)
            {
                var server = _servers.First(s => s.Id == id);
                if (server == null)
                    return 0;
                _servers.Remove(server);
                return 1;
            }

            public int Delete(string name)
            {
                var server = _servers.First(s => s.Name == name);
                if (server == null)
                    return 0;
                _servers.Remove(server);
                return 1;
            }

            public Server Read(int id)
            {
                throw new NotImplementedException();
            }

            public List<Server> ReadAll()
            {
                throw new NotImplementedException();
            }

            public Server ReadByName(string name)
            {
                throw new NotImplementedException();
            }

            public Server Update(Server server)
            {
                throw new NotImplementedException();
            }
        }
    }
}
