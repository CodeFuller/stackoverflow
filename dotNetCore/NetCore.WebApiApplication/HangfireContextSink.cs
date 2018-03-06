using System;
using Hangfire.Console;
using Serilog.Core;
using Serilog.Events;

namespace NetCore.WebApiApplication
{
	class HangfireContextSink : ILogEventSink
	{
		public const string PerformContextProperty = "PerformContext";

		private readonly IFormatProvider formatProvider;

		public HangfireContextSink(IFormatProvider formatProvider)
		{
			this.formatProvider = formatProvider;
		}

		public void Emit(LogEvent logEvent)
		{
			var message = logEvent.RenderMessage(formatProvider);

			LogEventPropertyValue propertyValue;
			if (logEvent.Properties.TryGetValue(PerformContextProperty, out propertyValue))
			{
				var context = (propertyValue as PerformContextProperty)?.PerformContext;
				context?.WriteLine(ConsoleTextColor.Green, DateTimeOffset.Now + " " + message);
			}
		}
	}
}
