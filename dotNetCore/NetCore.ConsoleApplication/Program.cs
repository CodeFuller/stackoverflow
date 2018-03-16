using System;
using TestLibrary;

namespace NetCore.ConsoleApplication
{
	public class SomeSettings
	{
		public string SomeValue { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Starting...");
				var context = new MyDbContext(@"Server=.\TestDB;Integrated Security=true;");
				context.Database.BeginTransaction();
				Console.WriteLine("Finished");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
