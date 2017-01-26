using System;
using System.Collections.Generic;

namespace Cake.NewRelic.API.Endpoints.Applications
{
    public class NewRelicApplication
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string HealthStatus { get; set; }
        public bool Reporting { get; set; }
        public DateTime LastReportedAt { get; set; }
        public ApplicationSummary ApplicationSummary { get; set; }
        public EndUserSummary EndUserSummary { get; set; }
        public Settings Settings { get; set; }
        public Links Links { get; set; }
    }

    public class ApplicationSummary
    {
        public float ResponseTime { get; set; }
        public float Throughput { get; set; }
        public float ErrorRate { get; set; }
        public float ApdexTarget { get; set; }
        public float ApdexScore { get; set; }
        public int HostCount { get; set; }
        public int InstanceCount { get; set; }
        public int ConcurrentInstanceCount { get; set; }
    }

    public class EndUserSummary
    {
        public float ResponseTime { get; set; }
        public float Throughput { get; set; }
        public float ApdexTarget { get; set; }
        public float ApdexScore { get; set; }
    }

    public class Settings
    {
        public float AppApdexThreshold { get; set; }
        public float EndUserApdexThreshold { get; set; }
        public bool EnableRealUserMonitoring { get; set;}
        public bool UserServerSideConfig { get; set; }
    }

    public class Links
    {
        public IEnumerable<uint> ApplicationInstances { get; set; }
        public IEnumerable<uint> Servers { get; set; }
        public IEnumerable<uint> ApplicationHosts { get; set; }
        public int AlertPolicy { get; set; }
    }
}
