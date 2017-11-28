using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCore.ConsoleApplication.Dal;

namespace NetCore.ConsoleApplication
{
	public class LoggingLevel
	{
		public LogLevel Default { get; set; }
	}

	public class LoggerSettings
	{
		public bool IncludeScopes { get; set; }

		public LoggingLevel LogLevel { get; set; }
	}

	public class ApplicationConfigurationSettings
	{
		public LoggerSettings Logging { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("AppSettings.json");
			IConfiguration configuration = configurationBuilder.Build();

			var settings = new ApplicationConfigurationSettings();
			configuration.GetSection("ApplicationConfiguration").Bind(settings);

			IServiceCollection services = new ServiceCollection();
			services.Configure<ApplicationConfigurationSettings>(configuration.GetSection("ApplicationConfiguration"));
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			ILoggerFactory loggerFactory = new LoggerFactory();
			loggerFactory.AddConsole();
			ILogger logger = loggerFactory.CreateLogger(String.Empty);
			logger.LogInformation("Hello :)");

			new MusicLibraryRepository().Test();
		}
	}
}
