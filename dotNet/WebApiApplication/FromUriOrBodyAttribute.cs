using System;

namespace WebApiApplication
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class FromUriOrBodyAttribute : Attribute
	{
	}
}
