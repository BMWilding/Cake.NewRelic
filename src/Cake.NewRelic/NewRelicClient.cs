using System;
using System.Net;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.NewRelic.API.Endpoints.Deployments;
using Cake.NewRelic.Result;
using RestSharp;

namespace Cake.NewRelic
{
    public class NewRelicClient
    {
        private readonly string _apiKey;
        private readonly int _appId;
        private readonly IRestClient _client;
        private readonly ICakeContext _context;
        private Uri BaseUri { get; } = new Uri("https://api.newrelic.com");

        public NewRelicClient(ICakeContext context, string apiKey, int appId, IRestClient client = null)
        {
            _apiKey = apiKey;
            _appId = appId;
            _context = context;
            _client = client ?? new RestClient(BaseUri);
        }

        // TODO: Finish implementing Application Query Member`
        public async Task<NewRelicResult> CreateDeployment(AppDeployment deployment)
        {
            var request = new CreateApplicationDeploymentRequest(deployment, _apiKey, _appId);

            var req = request.BuildRestRequest();
            var response = await _client.ExecuteTaskAsync(req);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    {
                        return new NewRelicResult(true, new ErrorResult());
                    }
                default:
                    {
                        return new NewRelicResult(false, new ErrorResult { StatusCode = (int)response.StatusCode, ErrorContent = response.Content});
                    }
            }
        }
    }
}