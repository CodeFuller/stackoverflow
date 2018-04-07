using Microsoft.AspNetCore.Builder;

namespace WebApiApplication
{
	public static class CustomMiddlewareExtensions
	{
		public static void UseCustomMiddleware(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseMiddleware<CustomMiddleware>();
		}
	}
}
