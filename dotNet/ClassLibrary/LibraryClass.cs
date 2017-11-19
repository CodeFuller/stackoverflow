using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
	public class LibraryClass
	{
		public void Test()
		{
			Console.WriteLine("Before JsonMediaTypeFormatter().SerializerSettings...");

			//	CF TEMP
			var assembly = Assembly.LoadFile(@"d:\Dropbox\prog\StackOverflow\dotNet\ConsoleApplication\bin\Debug\System.Net.Http.Formatting.dll");
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();

			Do();
			Console.WriteLine("After JsonMediaTypeFormatter().SerializerSettings!");
		}

		private void Do()
		{
			var settings = new JsonMediaTypeFormatter().SerializerSettings;
		}
	}
}
