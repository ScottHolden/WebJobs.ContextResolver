using System;

namespace WebJobs.ContextResolver
{
	public class ContextResolverTypeMismatchException : Exception
	{
		public ContextResolverTypeMismatchException(string typeName)
			: base($"Context resolver {typeName} was not of type {typeof(IContextResolver)}.")
		{
		}
	}
}