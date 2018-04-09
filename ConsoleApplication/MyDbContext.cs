using EfCoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApplication
{
	public class MyDbContext : DbContext
	{
		public DbSet<TestModel> Entries { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.;Database=TestDB;Trusted_Connection=True;");
		}
	}
}
