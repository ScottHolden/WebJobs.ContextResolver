using System;
using System.Collections.Concurrent;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace WebJobs.ContextResolver
{
	internal class ContextResolverTypedBindingProvider
	{
		private readonly IContextResolver m_resolver;
		private readonly ConcurrentDictionary<Type, IBinding> m_typedBindings = new ConcurrentDictionary<Type, IBinding>();

		public ContextResolverTypedBindingProvider(IContextResolver resolver)
		{
			m_resolver = resolver;
		}

		public IBinding ResolveBinding(Type parameterType)
		{
			return m_typedBindings.GetOrAdd(parameterType, BuildContextResolverBinding);
		}

		private IBinding BuildContextResolverBinding(Type type)
		{
			Type genericType = typeof(ContextResolverBinding<>).MakeGenericType(type);

			return (IBinding)Activator.CreateInstance(genericType, new object[] { m_resolver });
		}

		public static ContextResolverTypedBindingProvider FromContextResolverType(Type t)
		{
			if (!typeof(IContextResolver).IsAssignableFrom(t))
			{
				throw new ContextResolverTypeMismatchException(t.Name);
			}

			IContextResolver resolver = (IContextResolver)Activator.CreateInstance(t);

			return new ContextResolverTypedBindingProvider(resolver);
		}
	}
}