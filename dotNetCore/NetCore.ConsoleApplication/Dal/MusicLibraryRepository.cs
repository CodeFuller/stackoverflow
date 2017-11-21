using System;
using System.Collections.Generic;
using System.Linq;
using NetCore.ConsoleApplication.Objects;

namespace NetCore.ConsoleApplication.Dal
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
			var result = new MusicLibraryRepository().GetArtistAlbums(new Guid("47720456-A386-42F2-BF78-4466BD293AD0"));
		}
	}
}
