using System;
using Microsoft.Azure.WebJobs.Description;

namespace WebJobs.ContextResolver
{
	[AttributeUsage(AttributeTargets.Parameter)]
	[Binding]
	public class ResolveAttribute : Attribute
	{
	}
}