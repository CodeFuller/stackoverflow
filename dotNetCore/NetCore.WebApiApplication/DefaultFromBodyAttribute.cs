using System;

namespace NetCore.WebApiApplication
{
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class DefaultFromBodyAttribute : Attribute
{
}
}
