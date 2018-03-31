using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace ChildApplication
{
	public static class ChildApplicationMiddleware
	{
		public static IServiceCollection AddChildApplication(this IServiceCollection services)
		{
			services.AddMvc();

			services.Configure<RazorViewEngineOptions>(opt =>
			{
				opt.ViewLocationExpanders.Add(new ChildApplicationLocationRemapper());
			});

			return services;
		}

		public static IApplicationBuilder UseChildApplication(this IApplicationBuilder app)
		{
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "ChildApplication",
					template: "childapp/{controller=Home}/{action=Index}/{id?}",
					defaults: null,
					constraints: null,
					dataTokens: new { Namespace = "ChildApplication.ChildAppRoot.Controllers" });
			});

			return app;
		}
	}
}
