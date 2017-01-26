using System;
using RestSharp;

namespace Cake.NewRelic.API
{
    internal abstract class NewRelicRequest
    {
        public const string RootDomain = "https://api.newrelic.com/";
        public virtual string Endpoint { get; }
        public virtual Method Method { get; }
        public string ApiKey { get; protected set; }
        public int ApplicationId { get; protected set; }

        public virtual IRestRequest BuildRestRequest()
        {
            var request = new RestRequest(Endpoint);
            request.AddHeader("X-Api-Key", ApiKey);
            request.Method = Method;
            return request;
        }

        public virtual IRestRequest BuildRestRequest(Action<IRestRequest> requestModifier)
        {
            var request = BuildRestRequest();
            requestModifier(request);
            return request;
        }
    }
}
