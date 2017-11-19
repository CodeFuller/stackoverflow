using System;
using System.Reflection;
using Newtonsoft.Json;

namespace ConsoleApplication
{
	class Program
	{
		static void Run()
		{
			string output = JsonConvert.SerializeObject(new { Field = "Value" });
			Console.WriteLine($"Json: {output}");
		}

		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Started...");

				{
					var assembly = Assembly.LoadFile(@"d:\Dropbox\prog\StackOverflow\dotNet\ConsoleApplication\bin\Debug\4.5.0\Newtonsoft.Json.dll");
					var assemblies2 = AppDomain.CurrentDomain.GetAssemblies();
				}

				{
					//var assembly = Assembly.LoadFile(@"d:\CodeFuller\_days\2017.11.19\Json\9.0\Newtonsoft.Json.dll");
					var assemblies2 = AppDomain.CurrentDomain.GetAssemblies();
				}

				EmbeddedAssembly.Load("ConsoleApplication.Newtonsoft.Json.dll", "Newtonsoft.Json.dll");

				AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

				Run();

				var libAssembly = Assembly.LoadFile(@"d:\Dropbox\prog\StackOverflow\dotNet\ClassLibrary\bin\Debug\ClassLibrary.dll");
				var type = libAssembly.GetType("ClassLibrary.LibraryClass");
				if (type == null)
				{
					throw new InvalidOperationException("Class not found");
				}

				Console.WriteLine("Creating instance...");
				var obj = Activator.CreateInstance(type);
				Console.WriteLine("Invoking method...");
				var method = type.GetMethod("Test");

				var assemblies = AppDomain.CurrentDomain.GetAssemblies();

				method.Invoke(obj, null);
				
				Console.WriteLine("Finished successfully!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			Console.WriteLine($"CurrentDomain_AssemblyResolve({args.Name}) called");

			if (args.Name == "Newtonsoft.Json, Version=8.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed")
			{
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				return EmbeddedAssembly.Get(args.Name);
			}

			if (args.Name == "Newtonsoft.Json, Version=4.5.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed")
			{
				var assembly = Assembly.LoadFile(@"d:\Dropbox\prog\StackOverflow\dotNet\ConsoleApplication\bin\Debug\4.5.0\Newtonsoft.Json.dll");
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				return assembly;
			}
			
			throw new InvalidOperationException($"CurrentDomain_AssemblyResolve() called for unknown assembly: '{args.Name}'");
		}
	}
}
