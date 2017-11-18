using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Dal;

namespace WebApiApplication.Controllers
{
	[Produces("application/json")]
	[Route("api/Artist")]
	public class ArtistController : Controller
	{
		[HttpGet("{artistId}/Albums")]
		public async Task<IActionResult> GetArtistAlbums([FromRoute] Guid artistId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			using (var context = new MusicLibraryContext())
			{
				var albums = await context.Albums.Where(o => o.Artist.Id == artistId).ToListAsync();

				if (albums == null)
				{
					return NotFound();
				}

				return Ok(albums);
			}
		}
	}
}
