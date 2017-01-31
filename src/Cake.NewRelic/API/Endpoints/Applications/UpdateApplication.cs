using RestSharp;

namespace Cake.NewRelic.API.Endpoints.Applications
{
    internal class UpdateApplication : NewRelicRequest
    {
        public override string Endpoint => $"/v2/applications/{ApplicationId}.json";
        public override Method Method => Method.PUT;
    }
}