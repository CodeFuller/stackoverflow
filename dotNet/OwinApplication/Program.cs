using System;
using Microsoft.Owin.Hosting;

namespace OwinApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			string baseUri = "http://+:33335/";

			using (WebApp.Start<Startup>(baseUri))
			{
				Console.WriteLine($"Program startup, listening on {baseUri}");

				while (true)
				{
					Console.Write("Type QUIT to quit> ");
					string line = Console.ReadLine();
					if (line != null && line.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
					{
						break;
					}
				}
			}
		}
	}
}
