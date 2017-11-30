using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.ConsoleApplication.Dal;
using Serilog;
using Serilog.Events;

namespace NetCore.ConsoleApplication
{
	public class SubLoggerConfiguration
	{
		public LogEventLevel Level { get; set; }

		private string pathFormat;
		public string PathFormat
		{
			get => pathFormat;
			set => pathFormat = value.Replace("%APPLICATION_NAME%", Environment.GetEnvironmentVariable("APPLICATION_NAME"));
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			IServiceCollection services = new ServiceCollection();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			//ILoggerFactory loggerFactory = new LoggerFactory();
			//loggerFactory.AddConsole();
			//ILogger logger = loggerFactory.CreateLogger(String.Empty);
			//logger.LogInformation("Hello :)");

			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("AppSettings.json");
			IConfiguration configuration = configurationBuilder.Build();
			SubLoggerConfiguration subLoggerConfiguration = new SubLoggerConfiguration();
			configuration.GetSection("Serilog:SubLogger").Bind(subLoggerConfiguration);

			//var log = new LoggerConfiguration()
			//	.ReadFrom.Configuration(configuration)
			//	.CreateLogger();

			var Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == subLoggerConfiguration.Level).WriteTo.RollingFile(subLoggerConfiguration.PathFormat));

			var log = Logger.CreateLogger();
			log.Information("Information message");
			log.Warning("Warning message");
		}
	}
}
