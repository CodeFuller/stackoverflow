using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MvcApplication
{
	public static class MultiTenantHelper
	{
		public static IServiceProvider ServiceProvider { get; set; }

		public static HttpContext CurrentHttpContext => ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;

		public static HttpRequest CurrentRequest => CurrentHttpContext.Request;

		public static string CurrentRequestDomain => CurrentRequest.Host.Host;
	}
}
