using System.Data.Entity;
using ConsoleApplication.Objects;

namespace ConsoleApplication.Dal
{
	public class MusicLibraryContext : DbContext
	{
		public DbSet<Artist> Artists { get; set; }

		public DbSet<Album> Albums { get; set; }

		public MusicLibraryContext() :
			base("name=MusicLibraryContext")
		{
		}
	}
}
