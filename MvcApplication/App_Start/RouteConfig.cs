using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace MvcApplication
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			DefaultInlineConstraintResolver constraintResolver = new DefaultInlineConstraintResolver();
			constraintResolver.ConstraintMap.Add("testConstraint", typeof(TestConstraint));
			constraintResolver.ConstraintMap.Add("testConstraint2", typeof(TestConstraint2));
			routes.MapMvcAttributeRoutes(constraintResolver);
		}
	}
}
