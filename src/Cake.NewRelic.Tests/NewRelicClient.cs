using System.Net;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.NewRelic.API.Endpoints.Deployments;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;
using RestSharp;
using Xunit;

namespace Cake.NewRelic.Tests
{
    public class NewRelicClientTest
    {
        private readonly IFixture _fixture;

        public NewRelicClientTest()
        {
            _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            var cookieContainer = _fixture.Build<CookieContainer>()
                .With(x => x.PerDomainCapacity, 1)
                .With(x => x.Capacity, 1)
                .Create();

            _fixture.Inject(cookieContainer);
        }

        [Theory]
        [AutoData]
        public void CreateDeploymentTest(Mock<RestClient> client)
        {
            client.Setup(x => x.Execute(It.IsAny<RestRequest>()))
                .Returns(_fixture.Build<RestResponse>().With(x => x.StatusCode, HttpStatusCode.OK)
                    .Create());

            var sut = new NewRelicClient(
                _fixture.Create<ICakeContext>(),
                _fixture.Create<string>(),
                _fixture.Create<int>(),
                client.Object);

            sut.CreateDeployment(_fixture.Create<AppDeployment>());
            client.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once());
        }

        [Theory]
        [AutoData]
        public void CreateDeploymentFailureTest(Mock<RestClient> client)
        {
            var log = _fixture.Create<Mock<ICakeLog>>();
            _fixture.Inject(log.Object);

            var fakeContext = _fixture.Create<Mock<ICakeContext>>();
            client.Setup(x => x.Execute(It.IsAny<RestRequest>()))
                .Returns(_fixture.Build<RestResponse>().With(x => x.StatusCode, HttpStatusCode.BadRequest)
                    .Create());
            _fixture.Inject(fakeContext);

            var sut = _fixture.Create<NewRelicClient>();
            sut.CreateDeployment(_fixture.Create<AppDeployment>());

            client.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Never());
            log.Verify(
                x => x.Write(It.IsAny<Verbosity>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object>()),
                Times.AtLeastOnce());
        }
    }
}