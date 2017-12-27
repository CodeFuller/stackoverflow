using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.WebApiApplicationWithAuth.Data;
using NetCore.WebApiApplicationWithAuth.Models;
using NetCore.WebApiApplicationWithAuth.Services;
using Newtonsoft.Json.Serialization;

namespace NetCore.WebApiApplicationWithAuth
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
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			// Add API version control
			services.AddApiVersioning(options =>
			{
				options.ReportApiVersions = true;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.ErrorResponses = new DefaultErrorResponseProvider();
			});

			// Add and configure MVC services.
			services.AddMvc()
				.AddJsonOptions(setupAction =>
				{
					// Configure the contract resolver that is used when serializing .NET objects to JSON and vice versa.
					setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				});

			services.ConfigureApplicationCookie(options =>
			{
				options.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					return Task.CompletedTask;
				};
			});

			//services.ConfigureApplicationCookie(options =>
			//{
			//	options.Events.OnRedirectToLogin = async context =>
			//	{
			//		context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
			//		await context.Response.WriteAsync("Some custom error message if required");
			//	};
			//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			//app.UseStatusCodePagesWithRedirects("/error/index?errorCode={0}");
			app.UseStatusCodePages(context =>
			{
				if (context.HttpContext.Response.StatusCode != (int)HttpStatusCode.Unauthorized)
				{
					var location = string.Format(CultureInfo.InvariantCulture, "/error/index?errorCode={0}", context.HttpContext.Response.StatusCode);
					context.HttpContext.Response.Redirect(location);
				}
				return Task.CompletedTask;
			});

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
