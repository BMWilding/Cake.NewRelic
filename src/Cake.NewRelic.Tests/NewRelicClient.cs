using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.NewRelic.API.Endpoints.Deployments;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using RestSharp;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using LogLevel = Cake.Core.Diagnostics.LogLevel;
using Verbosity = Cake.Core.Diagnostics.Verbosity;

namespace Cake.NewRelic.Tests
{
    public class NewRelicClient_Test
    {
        private readonly IFixture _fixture;

        public NewRelicClient_Test()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
        }

        [Theory, AutoData]
        public void CreateDeploymentTest(
            string apiKey,
            int appId,
            Mock<RestClient> client)
        {
            var fakeContext = _fixture.Create<ICakeContext>();
            client.Setup(x => x.Execute(It.IsAny<RestRequest>()))
                .Returns(_fixture.Build<RestResponse>().With(x => x.StatusCode, HttpStatusCode.OK)
                .Create());
            var sut = new NewRelicClient(fakeContext, apiKey, appId, client.Object);
            sut.CreateDeployment(_fixture.Create<NewRelicApplicationDeployment>());
            client.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.AtLeastOnce());
        }

        [Theory, AutoData]
        public void CreateDeploymentFailureTest(
            string apiKey,
            int appId,
            Mock<RestClient> client)
        {
            var log = _fixture.Create<Mock<ICakeLog>>();
            _fixture.Inject(log.Object);
            var fakeContext = _fixture.Create<Mock<ICakeContext>>();
            client.Setup(x => x.Execute(It.IsAny<RestRequest>()))
                .Returns(_fixture.Build<RestResponse>().With(x => x.StatusCode, HttpStatusCode.BadRequest)
                .Create());
            var sut = new NewRelicClient(fakeContext.Object, apiKey, appId, client.Object);
            sut.CreateDeployment(_fixture.Create<NewRelicApplicationDeployment>());
            log.Verify(x => x.Write(It.IsAny<Verbosity>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object>()), Times.AtLeastOnce());
        }
    }
}
