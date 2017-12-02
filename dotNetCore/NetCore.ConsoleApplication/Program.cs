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
		public static Serilog.ILogger BuildLogger(IConfiguration config, SerilogSubLoggerConfigurations slcs)
		{
			var lc = new LoggerConfiguration();
			lc.ReadFrom.Configuration(config);

			foreach (var cfg in slcs.SubLoggers)
			{
				lc.WriteTo.Logger(logger => logger.Filter
					.ByIncludingOnly(lvl => lvl.Level == cfg.Level).WriteTo
					.RollingFile(cfg.PathFormat));
			}

			//	Apply any additional changes to lc configuration here

			//	Finally build a logger
			return lc.CreateLogger();
		}

		static void Main(string[] args)
		{
			IServiceCollection services = new ServiceCollection();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("AppSettings.json");
			IConfiguration configuration = configurationBuilder.Build();
			SerilogSubLoggerConfigurations subLoggerConfigurations = configuration.GetSection("Serilog").Get<SerilogSubLoggerConfigurations>();

			var logger = BuildLogger(configuration, subLoggerConfigurations);
			logger.Information("Test info message");
			logger.Warning("Test warning message");
			logger.Fatal("Test fatal message");
		}
	}
}
