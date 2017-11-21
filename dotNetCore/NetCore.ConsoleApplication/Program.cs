using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.ConsoleApplication.Dal;

namespace NetCore.ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("AppSettings.json");
			IConfiguration configuration = configurationBuilder.Build();

			IServiceCollection services = new ServiceCollection();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			ILoggerFactory loggerFactory = new LoggerFactory();
			loggerFactory.AddConsole();
			ILogger logger = loggerFactory.CreateLogger(String.Empty);
			logger.LogInformation("Hello :)");

			new MusicLibraryRepository().Test();
		}
	}
}
