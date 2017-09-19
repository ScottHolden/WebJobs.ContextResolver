using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace WebJobs.ContextResolver
{
	internal class ContextResolverBinding<T> : IBinding
	{
		private readonly IContextResolver m_contextResolver;
		public bool FromAttribute => true;

		public ContextResolverBinding(IContextResolver contextResolver)
		{
			m_contextResolver = contextResolver;
		}

		public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
		{
			return Task.Run<IValueProvider>(() => new ContextResolverValueProvider<T>(m_contextResolver, context.FunctionContext));
		}

		public Task<IValueProvider> BindAsync(BindingContext context)
		{
			return BindAsync(null, context.ValueContext);
		}

		public ParameterDescriptor ToParameterDescriptor()
		{
			return new ParameterDescriptor();
		}
	}
}