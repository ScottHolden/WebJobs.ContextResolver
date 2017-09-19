using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Practices.Unity;

namespace WebJobs.ContextResolver.Sample
{
	public class SampleResolver : IContextResolver
	{
		private readonly IUnityContainer m_container;

		public SampleResolver()
		{
			m_container = new UnityContainer();

			m_container.RegisterType<IService, SampleService>();
		}

		public T Resolve<T>(FunctionBindingContext context)
		{
			return m_container.Resolve<T>(new DependencyOverride<TraceWriter>(context.Trace));
		}
	}
}