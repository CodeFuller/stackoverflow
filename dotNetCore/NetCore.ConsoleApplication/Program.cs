using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var services = new ServiceCollection();
			services.AddSingleton<People>()
				.AddTransient(factory => (Func<Type, IPhone>)(type => (IPhone)factory.GetService(type)));

			foreach (var iphoneType in Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => !t.IsAbstract)
				.Where(t => typeof(IPhone).IsAssignableFrom(t)))
			{
				services.AddTransient(iphoneType);
			}

			var provider = services.BuildServiceProvider();
			provider.GetService<People>().Use<iphone5S>(phone => phone.Call("123456"));
		}
	}
}
