using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RazorPagesApplication
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
			services.AddMvc()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
					options => options.ResourcesPath = "Resources");
			services.AddLocalization(options => options.ResourcesPath = "Resources");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}
			var SupportedCultures = new List<CultureInfo> {
				new CultureInfo("en"),
				new CultureInfo("zh-Hans"),
				new CultureInfo("zh-Hant")
			};
			var options = new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en"),
				SupportedCultures = SupportedCultures,
				SupportedUICultures = SupportedCultures
			};
			app.UseRequestLocalization(options);
			app.UseStaticFiles();

			app.UseMvc();
		}
	}
}
