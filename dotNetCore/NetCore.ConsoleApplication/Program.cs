using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NetCore.ConsoleApplication
{
	public class LoggerEx<T> : ILogger<T>
	{
		private readonly ILogger _logger;

		public LoggerEx(ILoggerFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException(nameof(factory));
			}

			_logger = factory.CreateLogger(typeof(T).Assembly.GetName().Name);
		}

		IDisposable ILogger.BeginScope<TState>(TState state)
		{
			return _logger.BeginScope(state);
		}

		bool ILogger.IsEnabled(LogLevel logLevel)
		{
			return _logger.IsEnabled(logLevel);
		}

		void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			_logger.Log(logLevel, eventId, state, exception, formatter);
		}
	}

	public interface ISomeInterface
	{
		void SomeMethod();
	}

	public class SomeClass : ISomeInterface
	{
		private readonly ILogger<SomeClass> logger;

		public SomeClass(ILogger<SomeClass> logger)
		{
			this.logger = logger;
		}

		public void SomeMethod()
		{
			logger.LogInformation("Hello!");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var appConfiguration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false)
				.Build();

			var configuration = new LoggerConfiguration();
			configuration.ReadFrom.Configuration(appConfiguration);
			Log.Logger = configuration.CreateLogger();

			var services = new ServiceCollection();
			services.AddTransient<ISomeInterface, SomeClass>();
			services.AddLogging(lb =>
			{
				lb.AddSerilog();
			});
			services.AddSingleton(typeof(ILogger<>), typeof(LoggerEx<>));

			var serviceProvider = services.BuildServiceProvider();
			var obj = serviceProvider.GetService<ISomeInterface>();
			obj.SomeMethod();
		}
	}
}
