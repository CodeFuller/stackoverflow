using System;
using System.Linq;
using Serilog.Context;
using Serilog.Events;

namespace NetCore.ConsoleApplication
{
	public static class LogContextSerializer
	{
		public static LogContextDump Serialize()
		{
			var logContextEnricher = LogContext.Clone();
			var captureFactory = new CaptureLogEventPropertyFactory();
			logContextEnricher.Enrich(new LogEvent(DateTimeOffset.Now, LogEventLevel.Verbose, null, MessageTemplate.Empty, Enumerable.Empty<LogEventProperty>()), captureFactory);

			return captureFactory.Dump();
		}

		public static IDisposable Deserialize(LogContextDump contextDump)
		{
			return contextDump.PopulateLogContext();
		}
	}
}
