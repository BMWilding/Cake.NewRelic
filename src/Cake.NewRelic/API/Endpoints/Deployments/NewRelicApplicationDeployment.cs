namespace Cake.NewRelic.API.Endpoints.Deployments
{
    public class NewRelicApplicationDeployment
    {
        public AppDeployment deployment { get; set; }
    }

    // TODO: Replace RestSharp, as SerializeAs() is not supported
    public class AppDeployment
    {
        public string revision { get; set; }
        public string changeLog { get; set; }
        public string description { get; set; }
        public string user { get; set; }
    }
}