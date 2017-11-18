using Microsoft.EntityFrameworkCore;
using WebApiApplication.Models;

namespace WebApiApplication.Dal
{
	public class MusicLibraryContext : DbContext
	{
		public DbSet<Artist> Artists { get; set; }

		public DbSet<Album> Albums { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.;Database=TestMusicLibraryDB;Trusted_Connection=True;");
		}
	}
}
