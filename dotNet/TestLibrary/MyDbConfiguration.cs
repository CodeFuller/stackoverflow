using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace TestLibrary
{
	public class MyDbConfiguration : DbConfiguration
	{
		public MyDbConfiguration()
		{
			SetProviderFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
			SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
		}
	}
}
