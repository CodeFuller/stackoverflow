using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;

namespace NetCore.ConsoleApplication
{
	public class LogContextDump
	{
		public ICollection<PropertyValue> Properties { get; set; }

		public LogContextDump(IEnumerable<PropertyValue> properties)
		{
			Properties = new Collection<PropertyValue>(properties.ToList());
		}

		public IDisposable PopulateLogContext()
		{
			return LogContext.Push(Properties.Select(p => new PropertyEnricher(p.Name, p.Value, p.DestructureObjects) as ILogEventEnricher).ToArray());
		}
	}
}
