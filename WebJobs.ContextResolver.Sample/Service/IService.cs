using System.Threading.Tasks;

namespace WebJobs.ContextResolver.Sample
{
	public interface IService
	{
		Task<string> DoWorkAsync();
	}
}