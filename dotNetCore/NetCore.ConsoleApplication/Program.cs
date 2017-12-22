using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

namespace NetCore.ConsoleApplication
{
	public class MsSqlServerSinkConfiguration
	{
		public string ConnectionString { get; set; }

		public string TableName { get; set; }

		public bool AutoCreateSqlTable { get; set; }

		public ICollection<StandardColumn> ExcludedColumns { get; set; }

		public ColumnOptions ColumnOptions
		{
			get
			{
				ColumnOptions columnOptions = new ColumnOptions();
				foreach (var excludedColumn in ExcludedColumns)
				{
					columnOptions.Store.Remove(excludedColumn);
				}

				return columnOptions;
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var configBuilder = new ConfigurationBuilder();
			configBuilder.AddJsonFile("AppSettings.json");
			var configuration = configBuilder.Build();
			var sinkConfig = new MsSqlServerSinkConfiguration();
			configuration.Bind("SqlServerSink", sinkConfig);

			Logger log = new LoggerConfiguration()
				//	.WriteTo.MSSqlServer(@"Server=.;Database=LoggingDB;Trusted_Connection=True;", tableName, columnOptions: columnOptions, autoCreateSqlTable: true)
				.WriteTo.MSSqlServer(sinkConfig.ConnectionString, sinkConfig.TableName, columnOptions: sinkConfig.ColumnOptions, autoCreateSqlTable: sinkConfig.AutoCreateSqlTable)
				.CreateLogger();

			log.Information("Hello!");
			log.Dispose();
		}
	}
}
