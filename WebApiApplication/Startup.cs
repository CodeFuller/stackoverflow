using System;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
			app.UseExceptionHandler(GlobalErrorHandler);
			app.UseCustomMiddleware();
			app.UseMvc();
		}

		private void GlobalErrorHandler(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.Run(
				async context =>
				{
					context.Response.ContentType = "text/html";
					var ex = context.Features.Get<IExceptionHandlerFeature>();
					if (ex != null)
					{
						string errorMessage;
						var webFault = ex.Error as WebFaultException<string>;
						if (webFault != null)
						{
							context.Response.StatusCode = (int)webFault.StatusCode;
							errorMessage = webFault.Detail;
						}
						else
						{
							if (ex.Error is UnauthorizedAccessException)
							{
								context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
								errorMessage = string.Empty;
							}
							else
							{
								context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
								errorMessage = ex.Error.Message + new StackTrace(ex.Error, true).GetFrame(0).ToString();
							}

							//_logger.Error(errorMessage, ex.Error);
						}

						await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
					}
			});
		}
	}

	internal class WebFaultException<T> : Exception
	{
		public int StatusCode { get; set; }

		public string Detail { get; set; }
	}
}
