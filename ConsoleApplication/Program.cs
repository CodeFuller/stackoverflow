using System;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var childDllPath = @"d:\Dropbox\prog\StackOverflow\ChildApplication\bin\Debug\netcoreapp2.0\win-x64\publish\ChildApplication.dll";
			var startInfo = new ProcessStartInfo("dotnet", childDllPath + $" -dt {DateTime.Now.Date.ToString("yyyy-MM-dd")}")
			{
				WorkingDirectory = Path.GetDirectoryName(childDllPath),
			};

			Process.Start(startInfo);
		}
	}
}
