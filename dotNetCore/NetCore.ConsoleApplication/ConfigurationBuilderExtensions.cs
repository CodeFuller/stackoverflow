using Microsoft.Extensions.Configuration;

namespace NetCore.ConsoleApplication
{
	public static class ConfigurationBuilderExtensions
	{
		public static void AddInMemoryObject(this ConfigurationBuilder configurationBuilder, object settings, string settingsRoot)
		{
			configurationBuilder.AddInMemoryCollection(settings.ToKeyValuePairs(settingsRoot));
		}
	}
}
