using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;

namespace NetCore.WebApiApplication
{
	public class MoviesRedirectRule : IRule
	{
		private readonly string[] matchPaths;
		private readonly PathString newPath;

		public MoviesRedirectRule(string[] matchPaths, string newPath)
		{
			this.matchPaths = matchPaths;
			this.newPath = new PathString(newPath);
		}

		public void ApplyRule(RewriteContext context)
		{
			var request = context.HttpContext.Request;

			if (matchPaths.Contains(request.Path.Value))
			{
				request.Path = newPath;
				context.Result = RuleResult.SkipRemainingRules;
			}
		}
	}
}
