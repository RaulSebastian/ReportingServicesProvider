using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using ReportingServicesProvider.ServiceInterface.Repositories;
using ReportingServicesProvider.ServiceModel;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using ServiceStack.Text;
using Xunit;
using Platform = ReportingServicesProvider.ServiceModel.Types.Platform;

namespace ReportingServicesProvider.ServiceInterface.Tests
{
    [Trait("Category","Unit")]
    public class ServersServiceUnitTests : IDisposable
    {
        private readonly Guid _testId = Guid.NewGuid();
        private readonly DateTime _testExecutionStart = DateTime.Now;
        private readonly ServiceStackHost _appHost;
        private readonly ServersService _service;
        private ServersRepository _repository;

        public ServersServiceUnitTests()
        {
            Licensing.RegisterLicenseFromFileIfExists("~/ServiceStackLicense.txt".MapHostAbsolutePath());

            _appHost = new BasicAppHost(typeof(ServersService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    var dbFactory =
                        new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider)
                        {
                            AutoDisposeConnection = false
                        };
                    InitializeTestRepo(dbFactory);

                    _repository = new ServersRepository(dbFactory);

                    container.Register<IDbConnectionFactory>(dbFactory);
                    container.Register<IServersRepository>(_repository);
                }
            }
            .Init();
            _service = _appHost.Container.Resolve<ServersService>();
        }

        private void InitializeTestRepo(IDbConnectionFactory dbFactory)
        {
            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Server>();
                db.Save(new Server {Name = $"T1_{_testId}", Url = $"www1.{_testId}", Modified = _testExecutionStart});
                db.Save(new Server {Name = $"T2_{_testId}", Url = $"www2.{_testId}", Modified = _testExecutionStart});
                db.Save(new Server {Name = $"T3_{_testId}", Url = string.Empty, Active = false});
            }
        }

        private static Server BasicTestServer => new Server
        {
            Id = -1,
            Name = new Guid().ToString(),
            Url = string.Empty,
            Platform = DefaultValues.DefaultReportingPlatform
        };

        public void Dispose()
        {
            _repository.Dispose();

            _appHost.Dispose();
        }

        [Fact]
        public void GetServerList_should_return_initial_servers()
        {
            var response = (List<Server>)_service.Get(new GetServerList());
            var expected = new List<Server>
            {
                new Server {Name = $"T1_{_testId}", Url = $"www1.{_testId}", Modified = _testExecutionStart},
                new Server {Name = $"T2_{_testId}", Url = $"www2.{_testId}", Modified = _testExecutionStart}
            };

            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetServerNames_should_return_initial_servernames()
        {
            var response = _service.Get(new GetServerNames());
            var expected = new List<string>{ $"T1_{_testId}", $"T2_{_testId}" };

            response.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void GetServerById_should_return_corresponding_server()
        {
            var response = (Server)_service.Get(new GetServerById{ Id = 1});
            var expected = new Server
            {
                Id = 1,
                Name = $"T1_{_testId}",
                Url = $"www1.{_testId}",
                Modified = _testExecutionStart
            };

            response.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void GetServerByName_should_return_corresponding_server()
        {
            var response = (Server)_service.Get(new GetServerByName { Name = $"T1_{_testId}" });
            var expected = new Server
            {
                Id = 1,
                Name = $"T1_{_testId}",
                Url = $"www1.{_testId}",
                Modified = _testExecutionStart
            };

            response.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void PostServer_should_create_server()
        {
            var response = (Server) _service.Post(new PostServer
            {
                Name = $"Post_{_testId}",
                Platform = Platform.SqlServerReportingServices,
                Url = "www.post.ed"
            });
            var expected = new Server
            {
                Name = $"Post_{_testId}",
                Platform = Platform.SqlServerReportingServices,
                Url = "www.post.ed"
            };

            response.Id.Should().NotBe(0);
            response.Equals(expected).Should().BeTrue();
        }

        [Fact]
        public void PostServer_should_set_created_date()
        {
            var requestDate = DateTime.Now;
            var response = (Server)_service.Post(new PostServer
            {
                Name = $"CreateDate_{_testId}",
                Url = "www.ilikerandomurls.com"
            });

            response.Created.Should().BeCloseTo(requestDate);
        }

        [Fact]
        public void PostServer_without_platform_should_create_server_with_default_platform()
        {
            var response = (Server)_service.Post(new PostServer
            {
                Name = $"DefaultPlatform_{_testId}",
                Url = "www.defaultPlatform.test"
            });

            response.Platform.Should().Be(DefaultValues.DefaultReportingPlatform);
        }

        [Fact]
        public void PostServer_with_empty_name_should_throw_HttpError()
        {
            Action post = () => _service.Post(new PostServer
            {
                Name = $"T2_{_testId}",
                Url = "www.duplicateName.test"
            });

            post.ShouldThrow<HttpError>();
        }

        [Fact]
        public void PostServer_with_too_long_name_should_throw_HttpError()
        {
            Action post = () => _service.Post(new PostServer
            {
                Name = $"T2_{_testId}",
                Url = "www.duplicateName.test"
            });

            post.ShouldThrow<HttpError>();
        }

        [Fact]
        public void PostServer_with_existing_name_should_throw_HttpError()
        {
            Action post = () => _service.Post(new PostServer
            {
                Name = $"T2_{_testId}",
                Url = "www.duplicateName.test"
            });

            post.ShouldThrow<HttpError>();
        }

        [Fact]
        public void PostServer_with_invalid_platformId_should_throw_HttpError()
        {
            Action post = () => _service.Post(new PostServer
            {
                Name = $"PltfrmErr_{_testId}",
                Url = "www.justAnother.test",
                Platform = (Platform)99999999
            });

            post.ShouldThrow<HttpError>();
        }

        [Fact]
        public void PostServer_with_too_long_Name_should_throw_HttpError()
        {

            throw new NotImplementedException();
        }

        [Fact]
        public void PutServer_not_matching_existing_id_should_crete_new()
        {
            var nonExisting = _repository.GetById(-1);

            var response = (Server) _service.Put(
                new UpdateServerById
                {
                    Id = -1,
                    Name = string.Empty,
                    Url = string.Empty,
                    Platform = DefaultValues.DefaultReportingPlatform
                });

            nonExisting.Should().BeNull();
            response.Should().NotBeNull();
        }

        [Fact]
        public void PutServer_should_update_Name_for_existing_server()
        {
            var serverId = _repository.Save(BasicTestServer).Id;
            const string updatedName = "should be changed now";

            _service.Put(new UpdateServerById
            {
                Id = serverId,
                Name = updatedName,
                Url = string.Empty,
                Platform = DefaultValues.DefaultReportingPlatform
            });

            _repository.GetById(serverId).Name.ShouldBeEquivalentTo(updatedName);
        }

        [Fact]
        public void PutServer_should_update_Url_for_existing_server()
        {
            var serverId = _repository.Save(BasicTestServer).Id;
            const string updatedUrl = "www.new-awesome-url.com";

            _service.Put(new UpdateServerById
            {
                Id = serverId,
                Name = new Guid().ToString(),
                Url = updatedUrl,
                Platform = DefaultValues.DefaultReportingPlatform
            });

            _repository.GetById(serverId).Url.ShouldBeEquivalentTo(updatedUrl);
        }

        [Fact]
        public void PutServer_should_update_the_modified_date()
        {
            var newServer = _repository.Save(BasicTestServer);

            var serverId = newServer.Id;
            var creationDate = newServer.Created;

            _service.Put(new UpdateServerById
            {
                Id = serverId,
                Name = new Guid().ToString(),
                Url = string.Empty,
                Platform = DefaultValues.DefaultReportingPlatform
            });

            _repository.GetById(serverId).Modified.Should().BeAfter(creationDate);
        }
    }
}
