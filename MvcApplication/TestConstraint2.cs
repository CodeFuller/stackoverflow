﻿using System.Diagnostics;
using System.Web;
using System.Web.Routing;

namespace MvcApplication
{
	public class TestConstraint2 : IRouteConstraint
	{
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			Debug.WriteLine("TestConstraint2");
			return true;
		}
	}
}
