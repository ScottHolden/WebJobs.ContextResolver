using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace WebJobs.ContextResolver
{
	internal class ContextResolverValueProvider<T> : IValueProvider
	{
		private readonly IContextResolver m_resolver;
		private readonly FunctionBindingContext m_context;

		public ContextResolverValueProvider(IContextResolver resolver, FunctionBindingContext context)
		{
			m_resolver = resolver;
			m_context = context;
		}

		public Type Type
		{
			get
			{
				return typeof(T);
			}
		}

		public Task<object> GetValueAsync()
		{
			return Task.Run<object>(() => m_resolver.Resolve<T>(m_context));
		}

		public string ToInvokeString()
		{
			return null;
		}
	}
}