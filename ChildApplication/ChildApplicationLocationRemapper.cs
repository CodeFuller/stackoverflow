using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ChildApplication
{
	internal class ChildApplicationLocationRemapper : IViewLocationExpander
	{
		private readonly IEnumerable<string> preCompiledViewLocations;

		public ChildApplicationLocationRemapper()
		{
			// custom view locations for the pre-compiled views
			this.preCompiledViewLocations = new[]
			{
				"/ChildAppRoot/Views/{1}/{0}.cshtml",
				"/ChildAppRoot/Views/Shared/{1}/{0}.cshtml",
				"/ChildAppRoot/Views/Shared/{0}.cshtml",
			};
		}

		/// <inheritdoc />
		public void PopulateValues(ViewLocationExpanderContext context)
		{
			// check if we're trying to execute an action from a DashboardExample route
			if (context.ActionContext.ActionDescriptor.MatchesNamespaceInRoute(context))
			{
				/*
				 * by adding a value it identifies the view location is different from any similar locations in the user's project
				 * e.g. if the user also has a file at /Views/Home/Index.cshtml this will make sure that's not matched the same
				 * as ours at /DashboardExample/Views/Home/Index.cshtml even though they're both /Home/Index.cshtml views
				 */
				context.Values.Add("ChildAppRoot", bool.TrueString);
			}
		}

		/// <inheritdoc />
		public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
		{
			if (context.Values.TryGetValue("ChildAppRoot", out var isDashboardExample) && isDashboardExample == bool.TrueString)
			{
				return this.preCompiledViewLocations;
			}
			else
			{
				return viewLocations;
			}
		}
	}
}
