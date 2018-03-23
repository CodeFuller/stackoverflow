using System;
using System.Linq;
using Newtonsoft.Json;
using Serilog.Context;
using Serilog.Events;

namespace NetCore.ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			string jsonData;
			using (LogContext.PushProperty("property1", "SomeValue"))
			using (LogContext.PushProperty("property2", 123))
			{
				var dump = LogContextSerializer.Serialize();
				jsonData = JsonConvert.SerializeObject(dump);
			}

			//	Pass jsonData between processes

			var restoredDump = JsonConvert.DeserializeObject<LogContextDump>(jsonData);
			using (LogContextSerializer.Deserialize(restoredDump))
			{
				//  LogContext is the same as when Clone() was called above

				var logContextEnricher = LogContext.Clone();
				var factory = new CaptureLogEventPropertyFactory();
				logContextEnricher.Enrich(new LogEvent(DateTimeOffset.Now, LogEventLevel.Verbose, null, MessageTemplate.Empty, Enumerable.Empty<LogEventProperty>()), factory);
			}
		}
	}
}
