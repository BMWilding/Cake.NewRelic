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
        private readonly RestClient _client;
        private readonly ICakeContext _context;

        public NewRelicClient(ICakeContext context, string apiKey, int appId)
        {
            _apiKey = apiKey;
            _appId = appId;
            _context = context;
            _client = new RestClient("https://api.newrelic.com");
        }

        public void CreateDeployment(NewRelicApplicationDeployment config)
        {
            var request = new CreateApplicationDeploymentRequest(config, _apiKey, _appId);
            var response = _client.Execute(request.BuildRestRequest());
            if (response.StatusCode != HttpStatusCode.OK)
            {
                _context.Log.Write(Verbosity.Normal, LogLevel.Information, "Could not create New Relic deployment. HTTP Status Code: {0}", response.StatusCode);
            }
        }
    }
}
