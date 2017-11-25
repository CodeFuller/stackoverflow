using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCore.ConsoleApplication.Objects;

namespace NetCore.ConsoleApplication.Dal
{
	public class MusicLibraryContext : DbContext
	{
		public DbSet<Artist> Artists { get; set; }

		public DbSet<Album> Albums { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.;Database=TestMusicLibraryDB;Trusted_Connection=True;");

			var loggerFactory = new LoggerFactory();
			loggerFactory.AddDebug();
			optionsBuilder.UseLoggerFactory(loggerFactory);
			optionsBuilder.EnableSensitiveDataLogging();
		}
	}
}
