using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.WebApiApplication.Filters
{
	public class Filter2 : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			throw new NotImplementedException();
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			throw new NotImplementedException();
		}
	}
}
