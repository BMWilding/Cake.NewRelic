using System;
using System.Collections.Generic;

namespace Cake.NewRelic.API.Endpoints.Deployments
{
    public class NewRelicApplicationDeployment
    {
        public uint Id { get; set; }
        public string Revision { get; set; }
        public string ChangeLog { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEnumerable<int> Links { get; set; }
    }
}
