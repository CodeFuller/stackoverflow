using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
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
					options.FormatterMappings.SetMediaTypeMappingForFormat(
						"xml", MediaTypeHeaderValue.Parse("application/xml"));
					options.FormatterMappings.SetMediaTypeMappingForFormat(
						"json", MediaTypeHeaderValue.Parse("application/json"));
					options.FormatterMappings.SetMediaTypeMappingForFormat(
						"hateoas", MediaTypeHeaderValue.Parse("application/xml"));
					options.FormatterMappings.SetMediaTypeMappingForFormat(
						"xml-hateoas", MediaTypeHeaderValue.Parse("application/xml"));
					options.FormatterMappings.SetMediaTypeMappingForFormat(
						"json+hateoas", MediaTypeHeaderValue.Parse("application/json"));
				})
				.AddJsonOptions(options => {
					// Force Camel Case to JSON
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				})
				.AddXmlSerializerFormatters()
				.AddXmlDataContractSerializerFormatters()
				;
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
