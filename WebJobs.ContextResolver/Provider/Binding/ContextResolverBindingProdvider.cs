using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace WebJobs.ContextResolver
{
	internal class ContextResolverBindingProdvider : IBindingProvider
	{
		private ConcurrentDictionary<ParameterInfo, Type> m_parameterResolverTypes = new ConcurrentDictionary<ParameterInfo, Type>();
		private ConcurrentDictionary<Type, ContextResolverTypedBindingProvider> m_resolvers = new ConcurrentDictionary<Type, ContextResolverTypedBindingProvider>();

		public Task<IBinding> TryCreateAsync(BindingProviderContext context)
		{
			return Task.Run(() => TryCreate(context));
		}

		private IBinding TryCreate(BindingProviderContext context)
		{
			Type builder = m_parameterResolverTypes.GetOrAdd(context.Parameter, GetContextResolverType);

			ContextResolverTypedBindingProvider genericContextResolver = m_resolvers.GetOrAdd(builder, ContextResolverTypedBindingProvider.FromContextResolverType);

			return genericContextResolver.ResolveBinding(context.Parameter.ParameterType);
		}

		private Type GetContextResolverType(ParameterInfo parameter)
		{
			ContextResolverAttribute paramaterContainer = parameter.GetCustomAttribute<ContextResolverAttribute>();
			if (paramaterContainer?.Resolver != null)
			{
				return paramaterContainer.Resolver;
			}

			MethodInfo method = parameter.Member as MethodInfo;
			ContextResolverAttribute methodContainer = method.GetCustomAttribute<ContextResolverAttribute>();
			if (methodContainer?.Resolver != null)
			{
				return methodContainer.Resolver;
			}

			ContextResolverAttribute classContainer = method.DeclaringType.GetCustomAttribute<ContextResolverAttribute>();
			if (classContainer?.Resolver != null)
			{
				return classContainer.Resolver;
			}

			throw new ContextResolverNotFoundException($"{method.DeclaringType.Name}.{method.Name}.{parameter.Name}");
		}
	}
}