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
			using (var ctx = new CapabilitiesContext())
			{
				var list = new List<string> {"JAVA"};
				//var result = ctx.Resources.Select(x => x.ResourceCapability.Where(y => list.Contains(y.Capability.Description))).ToList();
				//var result = ctx.Resources.Where(x => ResourceCapability.Where(y => list.Contains(y.Capability.Description))).ToList();
				var result = ctx.ResourceCapabilities.Where(x => list.Contains(x.Capability.Description)).Select(x => x.Resource).Distinct().ToList();
			}
		}
	}
}
