using System;
using FluentAssertions;
using ReportingServicesProvider.ServiceModel.Requests;
using ReportingServicesProvider.ServiceModel.Requests.Servers;
using Xunit;
using ReportingServicesProvider.ServiceModel.Types;

namespace ReportingServicesProvider.ServiceModel.Tests
{
    [Trait("Category", "Unit")]
    public class RequestsMappersTests
    {
        [Fact]
        public void TestMethod1()
        {
            //var expected = new Server
            //{
            //    Id = 1,
            //    Active = true,
            //    Name = "test",
            //    Platform = Platform.SqlServerReportingServices,
            //    Url = "www"
            //};

            //var updateRequest = new UpdateServerById
            //{
            //    Id = 1,
            //    Platform = Platform.SqlServerReportingServices,
            //    Name = "test",
            //    Url = "www"
            //};

            //var result = updateRequest.ToDto();

            //result.Id.ShouldBeEquivalentTo(expected.Id);
            //result.Platform.ShouldBeEquivalentTo(expected.Platform);
            //result.Name.ShouldBeEquivalentTo(expected.Name);
            //result.Url.ShouldBeEquivalentTo(expected.Url);
        }
    }
}
