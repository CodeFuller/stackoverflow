using System;
using System.Threading;
using Hangfire.Console;
using Hangfire.Server;
using Serilog;
using Serilog.Context;

namespace NetCore.WebApiApplication
{
	public class TestJob
	{
		public static bool Execute(PerformContext context)
		{
			using (LogContext.PushProperty("Hangfirejob", "TestJob"))
			using (LogContext.Push(new PerformContextEnricher(context)))
			{
				try
				{
					var progress = context.WriteProgressBar("Progress");
					for (int i = 0; i < 10; i++)
					{
						context.WriteLine("Working with {0}", i);
						progress.SetValue((i + 1) * 10);
						Log.Debug("Test serilog", context);
						Log.Debug("Test from hangfirelog");
						Thread.Sleep(5000);
					}
					Log.Debug("Done testjob");
					return true;
				}
				catch (Exception ex)
				{
					Log.Error(ex, "Error!");
					return false;
				}
			}
		}
	}
}
