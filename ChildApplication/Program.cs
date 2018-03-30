using System.IO;
using Microsoft.Extensions.Configuration;

namespace ChildApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
			configurationBuilder.AddJsonFile("AppSettings.json", false);
			var configuration = configurationBuilder.Build();
			var settingValue = configuration.GetValue<string>("AppName");

			File.WriteAllText(@"d:\ChildAppOutput.txt", settingValue);
		}
	}
}
