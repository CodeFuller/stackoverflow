using Microsoft.Extensions.Configuration;

namespace NetCore.ConsoleApplication
{
	class Program
	{
		private static SomeSettings GetTestSettings()
		{
			return new SomeSettings
			{
				SomeKey1 = "SomeData",
				SomeKey2 = 123,
			};
		}

		static void Main(string[] args)
		{
			var configurationBuilder = new ConfigurationBuilder();
			var mySettings = GetTestSettings();
			configurationBuilder.AddInMemoryObject(mySettings, "Settings");

			var configuration = configurationBuilder.Build();
			SomeSettings settings = configuration.GetSection("Settings").Get<SomeSettings>();
		}
	}
}
