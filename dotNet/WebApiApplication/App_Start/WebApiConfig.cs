using System.Linq;
using System.Web.Http;

namespace WebApiApplication
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

config.ParameterBindingRules.Insert(0, paramDesc =>
{
	if (paramDesc.GetCustomAttributes<FromUriOrBodyAttribute>().Any())
	{
		return new UriOrBodyParameterBinding(paramDesc);
	}

	return null;
});
		}
	}
}
