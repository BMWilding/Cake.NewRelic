using RestSharp;

namespace Cake.NewRelic.API.Endpoints.Applications
{
    internal class DeleteApplication : NewRelicRequest
    {
        public override Method Method => Method.DELETE;
        public override string Endpoint => $"/v2/applicaions/{ApplicationId}.json";
    }
}