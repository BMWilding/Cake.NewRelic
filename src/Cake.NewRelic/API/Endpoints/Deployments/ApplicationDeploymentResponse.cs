using System.Collections.Generic;

namespace Cake.NewRelic.API.Endpoints.Deployments
{
    public abstract class ApplicationDeploymentResponse : INewRelicResponse
    {
        public IEnumerable<NewRelicApplicationDeployment> Deployments;
    }
}
