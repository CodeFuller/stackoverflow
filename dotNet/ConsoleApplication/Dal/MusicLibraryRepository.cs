using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Objects;

namespace ConsoleApplication.Dal
{
	public class MusicLibraryRepository
	{
		public IEnumerable<Album> GetArtistAlbums(Guid artistId)
		{
			using (var ctx = new MusicLibraryContext())
			{
				return ctx.Albums.Where(o => o.Artist.Id == artistId).ToList();
			}
		}

		public void Test()
		{
			using (var ctx = new MusicLibraryContext())
			{
				ctx.Database.Log = Console.Write;

				var artist = new Artist
				{
					Id = Guid.NewGuid(),
					Name = $"Created on {DateTime.Now}",
				};

				ctx.Artists.Add(artist);
				ctx.SaveChanges();
			}
		}
	}
}
