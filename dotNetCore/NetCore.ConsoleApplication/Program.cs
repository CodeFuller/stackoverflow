using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetCore.ConsoleApplication.Dal;
using Serilog;
using Serilog.Events;

namespace NetCore.ConsoleApplication
{
	public class SerilogSubLoggerConfigurations
	{
		public List<SerilogSubLoggerConfiguration> SubLoggers { get; set; }
	}

	public class SerilogSubLoggerConfiguration
	{
		private string _pathFormat;

		public LogEventLevel Level { get; set; }

		public string PathFormat
		{
			get => _pathFormat;
			set => _pathFormat = value.Replace("%APPLICATION_NAME%", Environment.GetEnvironmentVariable("APPLICATION_NAME"));
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			IServiceCollection services = new ServiceCollection();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("AppSettings.json");
			IConfiguration configuration = configurationBuilder.Build();
			SerilogSubLoggerConfigurations subLoggerConfigurations = configuration.GetSection("Serilog").Get<SerilogSubLoggerConfigurations>();
		}
	}
}
