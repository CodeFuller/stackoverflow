using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			bool useMapWhen = false;

			if (useMapWhen)
			{
				app.MapWhen(
					c => !c.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase),
					a =>
					{
						a.UseStatusCodePagesWithReExecute("/");
						a.UseMvc();
					});

				app.UseMvc();
			}
			else
			{
				var options = new RewriteOptions()
					.AddRewrite(@"^(?!/api)", "/api/values", skipRemainingRules: true);
				app.UseRewriter(options);

				app.UseMvc();
			}
		}
	}
}
