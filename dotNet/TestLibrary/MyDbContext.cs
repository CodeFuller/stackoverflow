using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
	[DbConfigurationType(typeof(MyDbConfiguration))]
	public class MyDbContext : DbContext, IMyDbContext
	{
		public MyDbContext(string connectionString)
			: base(connectionString)
		{
		}
	}
}
