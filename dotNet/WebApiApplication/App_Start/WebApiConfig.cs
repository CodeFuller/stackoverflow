using System.Web.Http;
using System.Web.Http.ModelBinding;

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

			var binderProvider = new CustomModelBinderProvider<string>(new EmptyStringToNullModelBinder());
			config.Services.Insert(typeof(ModelBinderProvider), 0, binderProvider);
		}
	}
}
