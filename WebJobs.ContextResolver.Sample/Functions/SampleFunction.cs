using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace WebJobs.ContextResolver.Sample
{
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
}