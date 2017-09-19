using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;

namespace WebJobs.ContextResolver.Sample
{
	public class SampleService : IService
	{
		private readonly TraceWriter m_log;

		public SampleService(TraceWriter log)
		{
			m_log = log;
		}

		public async Task<string> DoWorkAsync()
		{
			m_log.Info("Doing some work!");

			await Task.Delay(1000);

			return "Done";
		}
	}
}