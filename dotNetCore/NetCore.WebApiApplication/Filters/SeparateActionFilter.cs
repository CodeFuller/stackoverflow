using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.WebApiApplication.Filters
{
	public class SeparateActionFilter : IActionFilter
	{
		private enum FilterType
		{
			Mvc,
			WebApi
		}

		private readonly IActionFilter innerFilter;
		private readonly FilterType filterType;

		public static IActionFilter MvcFilter(IActionFilter innerFilter)
		{
			return new SeparateActionFilter(innerFilter, FilterType.Mvc);
		}

		public static IActionFilter WebApiFilter(IActionFilter innerFilter)
		{
			return new SeparateActionFilter(innerFilter, FilterType.WebApi);
		}

		private SeparateActionFilter(IActionFilter innerFilter, FilterType filterType)
		{
			this.innerFilter = innerFilter ?? throw new ArgumentNullException(nameof(innerFilter));
			this.filterType = filterType;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (ShouldExecute(context))
			{
				innerFilter.OnActionExecuting(context);
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			if (ShouldExecute(context))
			{
				innerFilter.OnActionExecuted(context);
			}
		}

		private bool ShouldExecute(FilterContext context)
		{
			return filterType == FilterType.Mvc && IsMvcFilter(context) ||
			       filterType == FilterType.WebApi && !IsMvcFilter(context);
		}

		private bool IsMvcFilter(FilterContext context)
		{
			//	You logic for separation of MVC and Web Api filters.
			//	You could make checks on context.ActionDescriptor, context.HttpContext or context.RouteData.

			return true;
		}
	}
}
