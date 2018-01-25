using System;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NetCore.ConsoleApplication
{
	public class MyObject
	{
	}

	public static class LoggerMessageEx
	{
		public static Action<ILogger, T1, Exception> DefineError<T1>(LogLevel logLevel, EventId eventId, string formatString)
		{
			var action = LoggerMessage.Define<T1, Exception>(logLevel, eventId, formatString);
			return (logger, arg1, exception) => action(logger, arg1, exception, exception);
		}
	}

	class Program
	{
//		private static readonly Action<ILogger, MyObject, Exception> _someEvent = LoggerMessage.Define<MyObject>(LogLevel.Information, new EventId(1), "Some Information about {MyObject}");

		private static readonly Action<ILogger, MyObject, Exception, Exception> _someEvent2 = LoggerMessage.Define<MyObject, Exception>(LogLevel.Information, new EventId(1), "Some Information about {MyObject} {@Exception}");

		private static readonly Action<ILogger, MyObject, Exception, Exception> _someErrorEvent =
			LoggerMessage.Define<MyObject, Exception>(
				LogLevel.Information,
				new EventId(1),
				"Some Information about {MyObject} {@Exception}");

		private static readonly Action<ILogger, MyObject, Exception> _someErrorEvent2 =
			LoggerMessageEx.DefineError<MyObject>(
				LogLevel.Information,
				new EventId(1),
				"Some Information about {MyObject} {@Exception}");

		static void Main(string[] args)
		{
			var configuration = new LoggerConfiguration();
			configuration.WriteTo.Console(new JsonFormatter());
			Log.Logger = configuration.CreateLogger();

			ILoggerFactory loggerFactory = new LoggerFactory();
			loggerFactory.AddSerilog();

			var logger = loggerFactory.CreateLogger("Test");
			//logger.LogInformation("Hello!");

			var exception = new InvalidOperationException("Hello there");

			//_someEvent(logger, new MyObject(), new InvalidOperationException());
			//_someEvent2(logger, new MyObject(), exception, exception);

			_someErrorEvent2(logger, new MyObject(), exception);
		}
	}
}
