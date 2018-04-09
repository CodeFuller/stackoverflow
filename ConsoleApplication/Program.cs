using System.Linq;

namespace EfCoreApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var ctx = new MyDbContext())
			{
				var data = ctx.Entries.ToList();
			}
		}
	}
}
