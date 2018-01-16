using System;
using System.Web.Http;
using WebApiApplication.Controllers;

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
				if (typeof(AuthenticatedRequest).IsAssignableFrom(paramDesc.ParameterType))
				{
					return new AuthRequestBinding(paramDesc);
				}

				return null;
			});
		}
	}
}
