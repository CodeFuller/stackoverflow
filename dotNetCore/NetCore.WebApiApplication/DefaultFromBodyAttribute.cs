using System;

namespace NetCore.WebApiApplication
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class DefaultFromBodyAttribute : Attribute
	{
	}
}
