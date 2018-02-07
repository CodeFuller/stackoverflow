using System.Web.Http;

namespace WebApiApplication
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			config.Routes.MapHttpRoute(
				name: "ActionApi",
				routeTemplate: "api/{controller}/{action}");
			// Web API routes

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultAdpi",
				routeTemplate: "api/ms/{controller}/{action}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "MSApi",
				routeTemplate: "api/{controller}/ms/{action}",
				defaults: new { controller = "GPI" }
			);

			config.MapHttpAttributeRoutes();
		}
	}
}
