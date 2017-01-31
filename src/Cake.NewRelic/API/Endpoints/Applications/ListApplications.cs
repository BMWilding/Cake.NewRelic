using System;
using System.Collections.Generic;
using RestSharp;

namespace Cake.NewRelic.API.Endpoints.Applications
{
    internal class ListApplicationsRequest : NewRelicRequest
    {
        public override string Endpoint => "/v2/applications.json";
        public override Method Method => Method.GET;
        public IEnumerable<Tuple<Filter, string>> Filters { get; set; }
        public IEnumerable<Tuple<Sort, string>> SortParams { get; set; }
    }

    internal class ListApplicationsResponse : INewRelicResponse
    {
        public IEnumerable<NewRelicApplication> Applications { get; set; }
    }

    public enum Filter
    {
        Name,
        Host,
        Id,
        Language
    }

    public enum Sort
    {
        HealthStatus,
        Token
    }
}