using System;
using System.Threading;
using System.Timers;
using ConsoleApplication.Dal;

namespace ConsoleApplication
{
	class Program
	{
		private static System.Timers.Timer timer;

		static void Main(string[] args)
		{
			timer = new System.Timers.Timer
			{
				AutoReset = false,
				Interval = 1000
			};
			timer.Elapsed += TimerTick;

			LogMessage("Starting timer...");
			timer.Start();
			Thread.Sleep(3000);

			LogMessage($"Timer is {(timer.Enabled ? "enabled" : "stopped")}");
			LogMessage("Setting AutoReset to true");
			timer.AutoReset = true;

			Thread.Sleep(Int32.MaxValue);
		}

		private static void TimerTick(object sender, ElapsedEventArgs e)
		{
			LogMessage($"Timer Tick: timer.Enabled is {timer.Enabled}");
		}

		private static void LogMessage(string message)
		{
			Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {message}");
		}
	}
}
