using System.Net;
using Cake.Core;
using Cake.NewRelic.API.Endpoints.Deployments;
using RestSharp;
using LogLevel = Cake.Core.Diagnostics.LogLevel;
using Verbosity = Cake.Core.Diagnostics.Verbosity;

namespace Cake.NewRelic
{
    public class NewRelicClient
    {
        private readonly string _apiKey;
        private readonly int _appId;
        private readonly IRestClient _client;
        private readonly ICakeContext _context;

        private const string BaseUri = "https://api.newrelic.com";

        public NewRelicClient(ICakeContext context, string apiKey, int appId, RestClient client = null)
        {
            _apiKey = apiKey;
            _appId = appId;
            _context = context;
            _client = client ?? new RestClient(BaseUri);
        }

        public void CreateDeployment(NewRelicApplicationDeployment config)
        {
            var request = new CreateApplicationDeploymentRequest(config, _apiKey, _appId);
            var req = request.BuildRestRequest();
            var response = _client.Execute(req);

            if (response.StatusCode == HttpStatusCode.OK) return;

            _context.Log.Write(Verbosity.Normal, LogLevel.Information, "Could not create New Relic deployment. HTTP Status Code: {0}", response.StatusCode);
            _context.Log.Write(Verbosity.Diagnostic, LogLevel.Information, "Error: {0}", response.Content);
        }
    }
}
