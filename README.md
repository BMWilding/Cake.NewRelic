Cake.NewRelic
------

This project is aimed towards triggering a deployment in NewRelic. 

Triggering a deployment is as easy as

```
#addin nuget:?package=Cake.NewRelic

Task("Register-NewRelic-Deployment")
.Does(() =>
{
    int id = 1; //Your NewRelic app ID
    string apikey = "SkeletonKey"; //Your NewRelic API Key
    NewRelicDeploy(apikey, id, new AppDeployment {
        revision = "264037",
        changelog = "Did some stuff",
        description = "Deployed the project to the environment",
        user = "brysonwilding@gmail.com"
    });
});

var target = Argument("target", "Build");
RunTarget(target);
```
