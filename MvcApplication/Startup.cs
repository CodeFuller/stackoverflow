using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MvcApplication
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

			var fileProviderInstance = new MultiTenantFileProvider();
			services.AddSingleton(fileProviderInstance);
			services.AddSingleton<IRazorViewEngine, MultiTenantRazorViewEngine>();

			//	Overriding singleton registration of IViewCompilerProvider
			services.AddTransient<IViewCompilerProvider, RazorViewCompilerProvider>();
			services.AddTransient<IRazorPageFactoryProvider, MultiTenantRazorPageFactoryProvider>();
			//	MultiTenantRazorPageFactoryProvider resolves DefaultRazorPageFactoryProvider by its type
			services.AddTransient<DefaultRazorPageFactoryProvider>();

			services.Configure<RazorViewEngineOptions>(options =>
			{
				//	Remove instance of PhysicalFileProvider
				options.FileProviders.Clear();
				options.FileProviders.Add(fileProviderInstance);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			MultiTenantHelper.ServiceProvider = app.ApplicationServices.GetRequiredService<IServiceProvider>();

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = app.ApplicationServices.GetRequiredService<MultiTenantFileProvider>()
			});

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
