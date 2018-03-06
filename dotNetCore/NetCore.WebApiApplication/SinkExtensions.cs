using System;
using Serilog;
using Serilog.Configuration;

namespace NetCore.WebApiApplication
{
	public static class SinkExtensions
	{
		public static LoggerConfiguration HangfireContextSink(this LoggerSinkConfiguration loggerSinkConfiguration, IFormatProvider formatProvider = null)
		{
			return loggerSinkConfiguration.Sink(new HangfireContextSink(formatProvider));
		}
	}
}
