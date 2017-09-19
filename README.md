# WebJobs.ContextResolver
A BYOB context resolver for use with DI/Containers

Sample is included.

```
[ContextResolver(typeof(SampleResolver))]
public class SampleFunction
{
    [FunctionName("SampleFunction")]
    public static async Task<string> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET")] HttpRequestMessage req,
        [Resolve] IService service)
    {
        return await service.DoWorkAsync();
    }
}
```

TODO: Improve documentation.