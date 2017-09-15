using System;
using System.Collections.Generic;
using FluentAssertions;
using ReportingServicesProvider.ServiceInterface.Repositories;
using ReportingServicesProvider.ServiceModel.Requests.Reports;
using ReportingServicesProvider.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using Xunit;

namespace ReportingServicesProvider.ServiceInterface.Tests
{
    [Trait("Category","Unit")]
    public class ReportsServiceUnitTests : IDisposable
    {
        private readonly Guid _testId = Guid.NewGuid();
        private readonly DateTime _testExecutionStart = DateTime.Now;
        private readonly ServiceStackHost _appHost;
        private readonly ReportsService _service;
        private readonly IReportsRepository _repository;
        private int _testServerId;
        private readonly int _initialTestReports = 5;

        public ReportsServiceUnitTests()
        {
            Licensing.RegisterLicenseFromFileIfExists("~/ServiceStackLicense.txt".MapHostAbsolutePath());

            _appHost = new BasicAppHost(typeof(ReportsService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    var dbFactory =
                        new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider)
                        {
                            AutoDisposeConnection = false
                        };
                    InitializeTestRepo(dbFactory);

                    container.Register<IDbConnectionFactory>(dbFactory);
                    container.Register<IReportsRepository>(new ReportsRepository(dbFactory));
                }
            }
            .Init();

            _service = _appHost.Container.Resolve<ReportsService>();
            _repository = _appHost.Container.Resolve<IReportsRepository>();
        }

        private void InitializeTestRepo(IDbConnectionFactory dbFactory)
        {
            using (var db = dbFactory.Open())
            {
                db.CreateTableIfNotExists<Report>();
                db.CreateTableIfNotExists<Server>();
                var server = new Server
                {
                    Name = $"ReportServer_{_testId}",
                    Url = $"www.{_testId}.net/sqlreports",
                    Modified = _testExecutionStart
                };
                db.Save(server);
                _testServerId = server.Id;
                for (var n = 1; n <= _initialTestReports; n++)
                {
                    db.Save(new Report
                    {
                        Server = _testServerId,
                        Name = $"T{n}_{_testId}.rdl",
                        Path = "/test",
                        Modified = _testExecutionStart
                    });
                }
            }
        }

        public void Dispose()
        {
            _repository.Dispose();
            _appHost.Dispose();
        }

        [Fact]
        public void GetReportList_should_return_initial_reports_count()
        {
            var response = (List<Report>)_service.Get(new GetReportList{Sid = _testServerId });
            
            response.Count.Should().Be(_initialTestReports);
        }

        [Fact]
        public void GetReportById_should_return_correct_initial_report()
        {
            var response = (Report)_service.Get(new GetReportById {Sid = _testServerId, Id = 1});

            var expected = new Report
            {
                Id = 1,
                Server = _testServerId,
                Name = $"T1_{_testId}.rdl",
                Path = "/test",
                Modified = _testExecutionStart
            };

            response.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void GetReportById_for_nonexisting_id_returns_nothing()
        {
            var response = _service.Get(new GetReportById { Sid = _testServerId, Id = 1337 });

            response.Should().BeNull();
        }

        [Fact]
        public void GetReportById_for_nonexisting_server_returns_nothing()
        {
            var response = _service.Get(new GetReportById { Sid = 1337, Id = 1 });

            response.Should().BeNull();
        }

        [Fact]
        public void PostReport_should_create_report()
        {
            var response = (Report) _service.Post(new PostReport
            {
                Sid = _testServerId,
                Name = $"Post_{_testId}.rdl",
                Path = "/test"
            });

            response.Id.Should().NotBe(0);
            response.Server.Should().Be(_testServerId);
            response.Name.Should().Be($"Post_{_testId}.rdl");
            response.Path.Should().Be("/test");
            response.Active.Should().BeTrue();
        }

        [Fact]
        public void PostReport_should_set_created_date()
        {
            var requestDate = DateTime.Now;
            var response = (Report)_service.Post(new PostReport
            {
                Sid = _testServerId,
                Name = $"CreateDate_{_testId}.rdl",
                Path = "/test"
            });

            response.Created.Should().BeCloseTo(requestDate);
        }

        [Fact]
        public void PostReport_with_too_long_name_should_throw_HttpError()
        {
            Action post = () => _service.Post(new PostReport
            {
                Sid = _testServerId,
                Name = new string('z', 500),
                Path = "/test"
            });

            post.ShouldThrow<HttpError>();
        }

        [Fact]
        public void PostReport_with_invalid_serverid_should_throw_HttpError()
        {
            Action post = () => _service.Post(new PostReport
            {
                Sid = 99999999,
                Name = $"MissingServer_{_testId}",
                Path = "/test"
            });

            post.ShouldThrow<HttpError>().WithMessage(ExceptionMessages.ServerNotFound);
        }

        [Fact]
        public void PutReport_not_matching_existing_id_should_crete_new()
        {
            var nonExisting = _repository.GetById(-1);

            var response = (Report)_service.Put(
                new UpdateReportById
                {
                    Id = -1,
                    Name = $"Upsert_{_testId}.rdl",
                    Path = "/test"
                });

            nonExisting.Should().BeNull();
            response.Should().NotBeNull();
            response.Id.Should().NotBe(-1);
        }

        [Fact]
        public void PutReport_should_update_Name_for_existing_server()
        {
            var createdReportId = _repository.Save(new Report
            {
                Name = $"_{_testId}.rdl",
                Path = "/test"
            }).Id;

            const string updatedName = "should be changed now";

            _service.Put(new UpdateReportById
            {
                Id = createdReportId,
                Name = updatedName,
                Path = "/test"
            });

            _repository.GetById(createdReportId).Name.ShouldBeEquivalentTo(updatedName);
        }
    }
}
