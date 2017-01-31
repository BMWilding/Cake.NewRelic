using RestSharp;

namespace Cake.NewRelic.API.Endpoints.Deployments
{
    internal class CreateApplicationDeploymentRequest : NewRelicRequest
    {
        public CreateApplicationDeploymentRequest(NewRelicApplicationDeployment deployment, string apiKey,
            int applicationId)
        {
            ApiKey = apiKey;
            ApplicationId = applicationId;
            Deployment = deployment;
        }

        public CreateApplicationDeploymentRequest(AppDeployment deployment, string apiKey, int applicationId)
            : this(new NewRelicApplicationDeployment {deployment = deployment}, apiKey, applicationId)
        {
        }

        public override string Endpoint => $"/v2/applications/{ApplicationId}/deployments.json";
        public override Method Method => Method.POST;
        public NewRelicApplicationDeployment Deployment { get; set; }

        public override IRestRequest BuildRestRequest()
        {
            var request = base.BuildRestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(Deployment);
            return request;
        }
    }
}