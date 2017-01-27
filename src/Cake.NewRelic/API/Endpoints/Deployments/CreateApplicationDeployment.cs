using System.Dynamic;
using RestSharp;

namespace Cake.NewRelic.API.Endpoints.Deployments
{
    internal class CreateApplicationDeploymentRequest : NewRelicRequest
    {
        public override string Endpoint => $"/v2/applications/{ApplicationId}/deployments.json";
        public override Method Method => Method.POST;
        public NewRelicApplicationDeployment Deployment { get; set; }

        public CreateApplicationDeploymentRequest(NewRelicApplicationDeployment deployment, string apiKey, int applicationId)
        {
            ApiKey = apiKey;
            ApplicationId = applicationId;
            Deployment = deployment;
        }

        public override IRestRequest BuildRestRequest()
        {
            dynamic reqObject = new ExpandoObject();
            reqObject.deployment = Deployment;
            var request = base.BuildRestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(reqObject);
            return request;
        }
    }
}
