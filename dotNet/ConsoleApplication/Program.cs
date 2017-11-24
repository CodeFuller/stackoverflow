using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> lstFiles = new List<string>()
			{
				"1",
				"2"
			};

			var tasks = lstFiles.Select(f => Task.Run(() => ActionOnFile(f))).ToArray();
			Task.WaitAll(tasks, TimeSpan.FromMinutes(2));

			Parallel.ForEach(lstFiles, file =>
			{
				var cts = new CancellationTokenSource(TimeSpan.FromMinutes(2));
				var task = Task.Run(() => ActionOnFile(file));
				try
				{
					task.Wait(cts.Token);
				}
				catch (OperationCanceledException)
				{
				}
			});

			//Parallel.ForEach(lstFiles, file =>
			//{
			//	// Doing some operation on file
			//	// Skip file and move to next if it is taking too long
			//});
		}

		static void ActionOnFile(string file)
		{
			Thread.Sleep(3000);
		}
	}
}
