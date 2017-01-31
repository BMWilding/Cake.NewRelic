using System;
using System.Net;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.NewRelic.API.Endpoints.Deployments;
using RestSharp;

namespace Cake.NewRelic
{
    public class NewRelicClient
    {
        private readonly string _apiKey;
        private readonly int _appId;
        private readonly IRestClient _client;
        private readonly ICakeContext _context;

        public NewRelicClient(ICakeContext context, string apiKey, int appId, IRestClient client = null)
        {
            _apiKey = apiKey;
            _appId = appId;
            _context = context;
            _client = client ?? new RestClient(BaseUri);
        }

        private Uri BaseUri { get; } = new Uri("https://api.newrelic.com");

        // TODO: Finish implementing Application Query Member`
        public void CreateDeployment(AppDeployment deployment)
        {
            var request = new CreateApplicationDeploymentRequest(deployment, _apiKey, _appId);

            var req = request.BuildRestRequest();
            var response = _client.Execute(req);

            if (response.StatusCode == HttpStatusCode.OK) return;

            _context.Log.Write(Verbosity.Normal, LogLevel.Information,
                "Could not create New Relic deployment. Response Code: {0}", response.StatusCode);
            _context.Log.Write(Verbosity.Diagnostic, LogLevel.Information, "Error: {0}", response.Content);
        }
    }
}