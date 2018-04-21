using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

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
			services.AddMvc(options =>
				{
					options.RespectBrowserAcceptHeader = true;
					options.ReturnHttpNotAcceptable = true;

					options.InputFormatters.Add(new XmlSerializerInputFormatter());

					var xmlOutputFormatter = new XmlSerializerOutputFormatter();
					xmlOutputFormatter.SupportedMediaTypes.Add("application/xml+hateoas");
					options.OutputFormatters.Add(xmlOutputFormatter);
				})
				.AddJsonOptions(options => {
					// Force Camel Case to JSON
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				})
				.AddXmlDataContractSerializerFormatters();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
