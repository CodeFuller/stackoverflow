using System.Collections.Generic;
using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace NetCore.ConsoleApplication
{
	public class CaptureLogEventPropertyFactory : ILogEventPropertyFactory
	{
		private readonly List<PropertyValue> values = new List<PropertyValue>();

		public LogEventProperty CreateProperty(string name, object value, bool destructureObjects = false)
		{
			values.Add(new PropertyValue(name, value, destructureObjects));
			return new LogEventProperty(name, new ScalarValue(value));
		}

		public LogContextDump Dump()
		{
			return new LogContextDump((values as IEnumerable<PropertyValue>).Reverse());
		}
	}
}
