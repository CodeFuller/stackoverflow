using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MvcApplication
{
	public class DoubleEscapingMiddleware
	{
		private readonly RequestDelegate _next;

		public DoubleEscapingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task InvokeAsync(HttpContext context)
		{
			var queryString = context.Request.QueryString;
			var decodedQueryString = WebUtility.UrlDecode(queryString.Value);
			context.Request.QueryString = new QueryString(decodedQueryString);

			var path = context.Request.Path;
			var decodedPath = WebUtility.UrlDecode(path);
			context.Request.Path = decodedPath;

			return _next(context);
		}
	}
}
