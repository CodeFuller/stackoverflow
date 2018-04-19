using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiApplication
{
	public class FooActionFilter : IActionFilter, IOrderedFilter
	{
		// Setting the order to 0, using IOrderedFilter, to attempt executing
		// this filter *before* the BaseController's OnActionExecuting.
		public int Order => Int32.MinValue;

		public void OnActionExecuting(ActionExecutingContext context)
		{
			// removed logic for brevity
			var foo = "bar";

			// Pass the extracted value back to the controller
			context.RouteData.Values.Add("foo", foo);
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}
