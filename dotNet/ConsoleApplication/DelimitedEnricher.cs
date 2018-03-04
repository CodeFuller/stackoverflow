using System;
using Serilog.Core;
using Serilog.Events;

namespace ConsoleApplication
{
	public class DelimitedEnricher : ILogEventEnricher
	{
		private readonly ILogEventEnricher innerEnricher;
		private readonly string innerPropertyName;
		private readonly string delimiter;

		public DelimitedEnricher(string innerPropertyName, string delimiter)
		{
			this.innerPropertyName = innerPropertyName;
			this.delimiter = delimiter;
		}

		public DelimitedEnricher(ILogEventEnricher innerEnricher, string innerPropertyName, string delimiter) : this(innerPropertyName, delimiter)
		{
			this.innerEnricher = innerEnricher;
		}

		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			innerEnricher?.Enrich(logEvent, propertyFactory);

			LogEventPropertyValue eventPropertyValue;
			if (logEvent.Properties.TryGetValue(innerPropertyName, out eventPropertyValue))
			{
				var value = (eventPropertyValue as ScalarValue)?.Value as string;
				if (!String.IsNullOrEmpty(value))
				{
					logEvent.AddPropertyIfAbsent(new LogEventProperty(innerPropertyName + "Delimited", new ScalarValue(value + delimiter)));
				}
			}
		}
	}
}
