using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication
{
	public class MyRouteAttribute : RouteAttribute
	{
		public static ApiRouteBaseConfiguration RouteConfiguration { get; } = new ApiRouteBaseConfiguration();

		public MyRouteAttribute() :
			base(RouteConfiguration.Url)
		{
		}

		public MyRouteAttribute(string route) :
			base(string.IsNullOrEmpty(route) ? RouteConfiguration.Url : $"{RouteConfiguration.Url}/" + route)
		{
		}
	}
}
