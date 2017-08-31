using System;
using ServiceStack;
using ServiceStack.Testing;
using ReportingServicesProvider;
using ReportingServicesProvider.ServiceInterface;
using Xunit;

namespace ReportingServicesProvider.Tests
{
    [Trait("Category","Unit")]
    public class UnitTests : IDisposable
    {
        private readonly ServiceStackHost appHost;

        public UnitTests()
        {
            appHost = new BasicAppHost(typeof(ServersService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                }
            }
            .Init();
        }

        public void Dispose()
        {
            appHost.Dispose();
        }

        [Fact]
        public void Test_Method1()
        {
            //var service = appHost.Container.Resolve<ServersService>();

            //var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            //Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }
    }
}
