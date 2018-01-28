using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace NetCore.ConsoleApplication
{
	public class Model
	{
	}

	public class Repository
	{
		public async Task<object> M(Model model, CancellationToken cancellationToken, SqlTransaction transaction = null)
		{
			object result = new Object();
			using (var sqlConnection = new SqlConnection(@"data source=localhost;initial catalog=TestData;persist security info=True;Integrated Security=SSPI;"))
			{
				sqlConnection.Open();
				using (transaction = sqlConnection.BeginTransaction())
				{
					result = await M1(sqlConnection, model, cancellationToken, transaction);
				}
			}
			return result;
		}

		internal async Task<object> M1(SqlConnection connection, Model model, CancellationToken cancellationToken, SqlTransaction transaction = null)
		{
			using (var multi = connection.QueryMultiple("SELECT * FROM Data", null, transaction))
			{
				throw new NotImplementedException();
			}
		}
	}
}
