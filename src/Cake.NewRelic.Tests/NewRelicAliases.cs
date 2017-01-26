using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.NewRelic.API.Endpoints.Deployments;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Xunit;

namespace Cake.NewRelic.Tests
{
    public class NewRelicAliases_Tests
    {
        private IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        [Fact]
        public void NewRelicDeploy_Test()
        {
            ICakeContext fakeContext = _fixture.Create<ICakeContext>();
            NewRelicAliases.NewRelicDeploy(fakeContext, _fixture.Create<string>(), _fixture.Create<int>(), _fixture.Create<NewRelicApplicationDeployment>());
        }
    }
}
