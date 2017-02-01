using Cake.Core;
using Cake.Core.Annotations;
using Cake.NewRelic.API.Endpoints.Deployments;

namespace Cake.NewRelic
{
    [CakeAliasCategory("NewRelic")]
    public static class NewRelicAliases
    {
        [CakeMethodAlias]
        [CakeNamespaceImport("Cake.NewRelic.API.Endpoints.Deployments")]
        public static void NewRelicDeploy(this ICakeContext context, string apiKey, int appId, AppDeployment deployment)
        {
            var client = new NewRelicClient(context, apiKey, appId);
            client.CreateDeployment(deployment);
        }
    }
}