using RestSharp;

namespace Cake.NewRelic.API.Endpoints.Deployments
{
    internal class GetApplicationDeploymentRequest : NewRelicRequest
    {
        public override string Endpoint => $"/v2/applications/{ApplicationId}/deployments.json";
        public override Method Method => Method.GET;
    }
}
