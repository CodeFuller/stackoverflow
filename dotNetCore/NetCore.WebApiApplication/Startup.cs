using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.WebApiApplication
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
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var rewrite = new RewriteOptions()
					.AddRedirect("Page1From", "Page1To")        // Redirect
					.AddRewrite("Page2From", "Page2To", true)  // Rewrite
				.Add(new MoviesRedirectRule(                // Custom Rule
				matchPaths: new[] { "/Page3From1", "/Page3From2", "/Page3From3" },
				newPath: "/api/values"));

			app.UseRewriter(rewrite);

			app.UseMvc();
		}
	}
}
