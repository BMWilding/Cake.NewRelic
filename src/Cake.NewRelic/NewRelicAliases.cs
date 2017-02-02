using Cake.Core;
using Cake.Core.Annotations;
using Cake.NewRelic.API.Endpoints.Deployments;
using LogLevel = Cake.Core.Diagnostics.LogLevel;
using Verbosity = Cake.Core.Diagnostics.Verbosity;

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
            var task = client.CreateDeployment(deployment);
            var response = task.Result;
            if (response.Successful)
            {
                context.Log.Write(Verbosity.Normal, LogLevel.Information, "NewRelic Deployment Successful");
                return;
            }
            context.Log.Write(Verbosity.Normal, LogLevel.Error, "NewRelic Deployment Failed with code: {0}", response.Error.StatusCode);
            context.Log.Write(Verbosity.Normal, LogLevel.Error, "{0}", response.Error.ErrorContent);
            context.Log.Write(Verbosity.Normal, LogLevel.Warning, "This only affects visibility in NewRelic. Other Cake Tasks will probably succeed.");
        }
    }
}