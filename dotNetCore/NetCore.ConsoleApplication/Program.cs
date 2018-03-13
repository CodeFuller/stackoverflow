using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			//var ServiceTypeName = LoadServiceAssembly(AssemblyName);

			//	Read from config
			var assemblyPath = "...";
			var typeName = "...";

			var assembly = Assembly.LoadFrom(assemblyPath);
			var loggerType = assembly.GetType(typeName);
			
			var serviceProvider = new ServiceCollection()
				.AddTransient(typeof(IDILogger), loggerType)
				.BuildServiceProvider();

			var logger = serviceProvider.GetService<IDILogger>();
		}
	}
}
