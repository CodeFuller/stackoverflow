using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace WebApiApplication
{
	public class SubdomainRouter : IRouter
	{
		private readonly Regex hostRegex = new Regex(@"^(.+?)\.(.+)$", RegexOptions.Compiled);

		private readonly IRouter defaultRouter;
		private readonly string domain;
		private readonly string controllerName;
		private readonly string actionName;

		public SubdomainRouter(IRouter defaultRouter, string domain, string controllerName, string actionName)
		{
			this.defaultRouter = defaultRouter;
			this.domain = domain;
			this.controllerName = controllerName;
			this.actionName = actionName;
		}

		public async Task RouteAsync(RouteContext context)
		{
			var request = context.HttpContext.Request;
			var hostname = request.Host.Host;

			var match = hostRegex.Match(hostname);
			if (match.Success && String.Equals(match.Groups[2].Value, domain, StringComparison.OrdinalIgnoreCase))
			{
				var routeData = new RouteData();
				routeData.Values["controller"] = controllerName;
				routeData.Values["action"] = actionName;
				routeData.Values["username"] = match.Groups[1].Value;

				context.RouteData = routeData;
				await defaultRouter.RouteAsync(context);
			}
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			return defaultRouter.GetVirtualPath(context);
		}
	}
}
