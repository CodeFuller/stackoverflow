using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;

namespace ChildApplication
{
	internal static class RouteContextExtensions
	{
		private static bool MatchesNamespaceInRoute(ActionDescriptor action, RouteData routeData)
		{
			var dataTokenNamespace = (string)routeData.DataTokens.FirstOrDefault(dt => dt.Key == "Namespace").Value;
			var actionNamespace = ((ControllerActionDescriptor)action).MethodInfo.DeclaringType.Namespace;

			return dataTokenNamespace == actionNamespace;
		}

		public static bool MatchesNamespaceInRoute(this ActionDescriptor action, RouteContext routeContext)
		{
			return MatchesNamespaceInRoute(action, routeContext.RouteData);
		}

		public static bool MatchesNamespaceInRoute(this ActionDescriptor action, ViewLocationExpanderContext viewLocationExpanderContext)
		{
			return MatchesNamespaceInRoute(action, viewLocationExpanderContext.ActionContext.RouteData);
		}
	}
}
