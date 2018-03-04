using Serilog;
using Serilog.Context;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var outputTemplate = "{MachineNameDelimited}{EnvironmentUserNameDelimited}{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] | {ClassNameDelimited}{Message:l}{NewLine}{Exception}";
			var Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console(outputTemplate: outputTemplate)
				.WriteTo.File("logFile-.log",
					outputTemplate: outputTemplate)
				.Enrich.FromLogContext()
				.Enrich.WithEnvironmentUserNameDelimited()
				.Enrich.WithMachineNameDelimited()
				.Enrich.WithPropertyDelimited("ClassName")
				.Enrich.WithProperty("Version", "1.0.0")
				.CreateLogger();

			Logger.Information("Hello, Serilog!");

			var position = new { Latitude = 25, Longitude = 134 };
			var elapsedMs = 35;

			for (int i = 0; i < 5; i++)
			{
				Logger.Information("Processed {@position} in {elapsed} ms.", position, elapsedMs);
				Logger.Information("");
			}

			using (LogContext.PushProperty("ClassName", "TestClass"))
			{
				for (int i = 0; i < 5; i++)
				{
					Logger.Information("Processed {@position} in {elapsed} ms.", position, elapsedMs);
					Logger.Information("");
				}
			}
		}
	}
}
