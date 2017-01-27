using System;
using System.Collections.Generic;

namespace Cake.NewRelic.API.Endpoints.Deployments
{
    public class NewRelicApplicationDeployment
    {
        public string revision { get; set; }
        public string changelog { get; set; }
        public string description { get; set; }
        public string user { get; set; }
    }
}
