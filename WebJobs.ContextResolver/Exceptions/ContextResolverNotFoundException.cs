using System;

namespace WebJobs.ContextResolver
{
	internal class ContextResolverNotFoundException : Exception
	{
		public ContextResolverNotFoundException(string parameterName)
			: base($"Couldn't find context resolver for {parameterName}")
		{
		}
	}
}