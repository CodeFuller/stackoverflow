using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace ConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			using (MySqlConnection connection = new MySqlConnection("server=localhost;user id=testuser;password=testuser;database=test;UseAffectedRows=True"))
			{
				var p = new DynamicParameters();
				p.Add("@v_changed_rows", dbType: DbType.Int32, direction: ParameterDirection.Output);

				connection.Execute("test_v1", p, commandType: CommandType.StoredProcedure);

				int answer = p.Get<int>("@v_changed_rows");
			}
		}
	}
}
