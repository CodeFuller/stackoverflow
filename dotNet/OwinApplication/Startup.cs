using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.ExceptionHandling;
//using System.Web.Http.Routing;
//using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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

			ConfigureAuth(app);
		}

		public static void ConfigureAuth(IAppBuilder app)
		{
			//CookieAuthenticationProvider provider = new CookieAuthenticationProvider();

			//var originalHandler = provider.OnApplyRedirect;

			//provider.OnApplyRedirect = context =>
			//{
			//	var url = HttpContext.Current.Request.Url.AbsoluteUri;


			//	var mvcContext = new HttpContextWrapper(HttpContext.Current);
			//	var routeData = RouteTable.Routes.GetRouteData(mvcContext);

			//	//Get the current language  
			//	RouteValueDictionary routeValues = new RouteValueDictionary();
			//	routeValues.Add("lang", routeData.Values["lang"]);

			//	//Reuse the RetrunUrl
			//	Uri uri = new Uri(context.RedirectUri);
			//	string returnUrl = HttpUtility.ParseQueryString(uri.Query)[context.Options.ReturnUrlParameter];
			//	routeValues.Add(context.Options.ReturnUrlParameter, returnUrl);

			//	//Overwrite the redirection uri
			//	//context.RedirectUri = url.Action("login", "account", routeValues);
			//	originalHandler.Invoke(context);
			//};


			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "ApplicationCookie",
				LoginPath = new PathString("/auth/login"),
				Provider = new CookieAuthenticationProvider { OnApplyRedirect = OnApplyRedirect }
			});
		}

		public static void OnApplyRedirect(CookieApplyRedirectContext context)
		{
			var url = HttpContext.Current.Request.Url.AbsoluteUri;

			string redirectUrl = "/auth/login";
			if (url.ToLower().Contains("/su/"))
			{
				redirectUrl = "/su/auth/login";
			}
			else if (url.ToLower().Contains("/app/"))
			{
				redirectUrl = "/app/auth/login";
			}

			context.Response.Redirect(redirectUrl);
		}
	}
}
