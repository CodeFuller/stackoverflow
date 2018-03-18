using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Web.Http.Routing;

namespace WebApiApplication
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			var constraintResolver = new DefaultInlineConstraintResolver()
			{
				ConstraintMap =
				{
					["apiVersion"] = typeof( ApiVersionRouteConstraint )
				}
			};

			// Web API routes
			config.MapHttpAttributeRoutes(constraintResolver);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
