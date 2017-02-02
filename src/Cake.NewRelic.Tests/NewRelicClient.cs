using System;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        public async void CreateDeploymentTest(Mock<RestClient> client)
        {
            IRestResponse response = _fixture.Build<RestResponse>().With(x => x.StatusCode, HttpStatusCode.OK).Create();
            var tResponse = Task.FromResult(response);
            client.Setup(x => x.ExecuteTaskAsync(It.IsAny<RestRequest>()))
                .Returns(tResponse);
            client.Setup(x => x.BaseUrl).Returns(new Uri("http://fake.domain"));

            var sut = new NewRelicClient(
                _fixture.Create<ICakeContext>(),
                _fixture.Create<string>(),
                _fixture.Create<int>(),
                client.Object);

            await sut.CreateDeployment(_fixture.Create<AppDeployment>());
            client.Verify(x => x.ExecuteTaskAsync(It.IsAny<RestRequest>()), Times.Once());
        }

        [Theory]
        [AutoData]
        public async void CreateDeploymentFailureTest(Mock<RestClient> client)
        {
            IRestResponse response = _fixture.Build<RestResponse>().With(x => x.StatusCode, HttpStatusCode.BadRequest).Create();
            var tResponse = Task.FromResult(response);
            client.Setup(x => x.ExecuteTaskAsync(It.IsAny<RestRequest>())).Returns(tResponse);
            _fixture.Inject<IRestClient>(client.Object);

            var sut = _fixture.Create<NewRelicClient>();
            var result = await sut.CreateDeployment(_fixture.Create<AppDeployment>());

            client.Verify(x => x.ExecuteTaskAsync(It.IsAny<RestRequest>()), Times.Once());
            Assert.False(result.Successful);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.Error.StatusCode);
        }
    }
}