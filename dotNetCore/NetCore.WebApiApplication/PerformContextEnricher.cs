using Hangfire.Server;
using Serilog.Core;
using Serilog.Events;

namespace NetCore.WebApiApplication
{
	public class PerformContextEnricher : ILogEventEnricher
	{
		private readonly PerformContext performContext;

		public PerformContextEnricher(PerformContext performContext)
		{
			this.performContext = performContext;
		}

		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			logEvent.AddPropertyIfAbsent(new LogEventProperty(HangfireContextSink.PerformContextProperty, new PerformContextProperty(performContext)));
		}
	}
}
