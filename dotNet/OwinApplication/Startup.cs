using System.Net.Http.Headers;
using System.Web.Http;
using Owin;

namespace OwinApplication
{
	/// <summary>
	/// Startup configuration logic for REST API
	/// </summary>
	internal class Startup
	{
		// Suppressing this message, which is triggered because HttpConfiguration is IDispoable.  But if
		// we dispose of this configuration on startup, our program won't work!
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public static void Configuration(IAppBuilder app)
		{
			// Configure Web API for self-host. 
			HttpConfiguration config = new HttpConfiguration();

			// Enable specification of attributes like [RoutePrefix], [Route], etc. in controller classes
			config.MapHttpAttributeRoutes();

			// Configure JSON response from REST API
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			app.UseWebApi(config);

			//var url = HttpContext.Current.Request.Url.AbsoluteUri;
			//string loginPath = "/auth/login";
			//string areaName = string.Empty;
			//if (url.ToLower().Contains("/su/"))
			//{
			//	areaName = "SU";
			//	loginPath = "/su/auth/login";
			//}
			//if (url.ToLower().Contains("/app/"))
			//{
			//	areaName = "APP";
			//	loginPath = "/app/auth/login";
			//}
			//app.UseCookieAuthentication(new CookieAuthenticationOptions
			//{
			//	AuthenticationType = "ApplicationCookie" + areaName,
			//	LoginPath = new PathString(loginPath)
			//});
		}
	}
}
