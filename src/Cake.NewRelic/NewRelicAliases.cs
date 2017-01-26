using Cake.Core;
using Cake.Core.Annotations;
using Cake.NewRelic.API.Endpoints.Deployments;

namespace Cake.NewRelic
{
    [CakeAliasCategory("NewRelic")]
    public static class NewRelicAliases
    {
        [CakeMethodAlias]
        public static void NewRelicDeploy(ICakeContext context, string apiKey, int appId, NewRelicApplicationDeployment deployment)
        {
            var client = new NewRelicClient(context, apiKey, appId);
            client.CreateDeployment(deployment);
        }
    }
}
