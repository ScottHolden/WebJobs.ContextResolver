using Microsoft.Azure.WebJobs.Host.Bindings;

namespace WebJobs.ContextResolver
{
	public interface IContextResolver
	{
		/// <summary>
		/// Resolves an instance of type 'T' with respect to the current context.
		/// </summary>
		/// <typeparam name="T">The generic type to resolve</typeparam>
		/// <param name="context">The context of the current resolution</param>
		/// <returns>An instance of 'T'</returns>
		T Resolve<T>(FunctionBindingContext context);
	}
}