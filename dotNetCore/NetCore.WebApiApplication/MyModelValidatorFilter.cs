using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.WebApiApplication
{
	public class MyModelValidatorFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext actionContext)
		{
			if (actionContext.ModelState.IsValid)
				return;

			var errors = new Dictionary<string, string[]>();

			foreach (var err in actionContext.ModelState)
			{
				var itemErrors = new List<string>();

				foreach (var error in err.Value.Errors)
				{
					itemErrors.Add(error.Exception.Message);
				}

				errors.Add(err.Key, itemErrors.ToArray());
			}

			actionContext.Result = new OkObjectResult(new MyResponse
			{
				Errors = errors
			});
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			throw new NotImplementedException();
		}
	}

	public class MyResponse
	{
		public Dictionary<string, string[]> Errors { get; set; }
	}
}
