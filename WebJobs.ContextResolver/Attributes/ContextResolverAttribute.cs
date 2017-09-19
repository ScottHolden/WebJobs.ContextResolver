using System;

namespace WebJobs.ContextResolver
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter)]
	public class ContextResolverAttribute : Attribute
	{
		public Type Resolver { get; }

		public ContextResolverAttribute(Type resolver)
		{
			Resolver = resolver;
		}
	}
}