using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var connectionString = ConfigurationManager.AppSettings["serilog:write-to:MSSqlServer.connectionString"];
			var tableName = ConfigurationManager.AppSettings["serilog:write-to:MSSqlServer.tableName"];
			var autoCreateSqlTable = Convert.ToBoolean(ConfigurationManager.AppSettings["serilog:write-to:MSSqlServer.autoCreateSqlTable"]);
			var excludedColumns = ConfigurationManager.AppSettings["serilog:write-to:MSSqlServer.excludedColumns"];

			ColumnOptions columnOptions = new ColumnOptions();
			foreach (var excludedColumn in Regex.Split(excludedColumns, ",\\s*"))
			{
				columnOptions.Store.Remove((StandardColumn)Enum.Parse(typeof(StandardColumn), excludedColumn, true));
			}

			Logger log = new LoggerConfiguration()
				.WriteTo.MSSqlServer(connectionString, tableName, columnOptions: columnOptions, autoCreateSqlTable: autoCreateSqlTable)
				.CreateLogger();
			log.Information("Hello!");
			log.Dispose();
		}
	}
}
