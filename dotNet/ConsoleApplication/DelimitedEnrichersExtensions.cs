using Serilog;
using Serilog.Configuration;
using Serilog.Enrichers;

namespace ConsoleApplication
{
	public static class DelimitedEnrichersExtensions
	{
		public const string Delimiter = " | ";

		public static LoggerConfiguration WithEnvironmentUserNameDelimited(this LoggerEnrichmentConfiguration enrichmentConfiguration)
		{
			return enrichmentConfiguration.With(new DelimitedEnricher(new EnvironmentUserNameEnricher(), EnvironmentUserNameEnricher.EnvironmentUserNamePropertyName, Delimiter));
		}

		public static LoggerConfiguration WithMachineNameDelimited(this LoggerEnrichmentConfiguration enrichmentConfiguration)
		{
			return enrichmentConfiguration.With(new DelimitedEnricher(new MachineNameEnricher(), MachineNameEnricher.MachineNamePropertyName, Delimiter));
		}

		public static LoggerConfiguration WithPropertyDelimited(this LoggerEnrichmentConfiguration enrichmentConfiguration, string propertyName)
		{
			return enrichmentConfiguration.With(new DelimitedEnricher(propertyName, Delimiter));
		}
	}
}
