using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();

			var container = new StructureMap.Container(
				c =>
				{
					c.For(typeof(ILogger<>)).Use(typeof(Logger<>));
					c.For(typeof(ILoggerFactory)).Use(loggerFactory);
				});
			
			//loggerFactory.AddAWSProvider(Configuration.GetAWSLoggingConfigSection(),
			//(logLevel, message, exception) => $"[{DateTime.UtcNow}] {logLevel}: {message}");
		}
	}
}
