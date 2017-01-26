using RestSharp;

namespace Cake.NewRelic.API.Endpoints.Deployments
{
    internal class DeleteApplicationDeploymentRequest : NewRelicRequest
    {
        public override string Endpoint => $"/v2/applications/{ApplicationId}/deployments/{ApplicationId}.json";
        public override Method Method => Method.DELETE;
    }
}
