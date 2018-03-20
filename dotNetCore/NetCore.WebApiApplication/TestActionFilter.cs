using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.WebApiApplication
{
	public class TestActionFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			return;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var controller = context.Controller as Controller;
			var statusCode = controller.Response.StatusCode;
		}
	}
}
