using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class Manufacturer2Controller : Controller
	{
		private readonly IDbObjectFactory dbObjectFactory;

		public Manufacturer2Controller(IDbObjectFactory dbObjectFactory)
		{
			this.dbObjectFactory = dbObjectFactory ?? throw new ArgumentNullException(nameof(dbObjectFactory));
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var manufacturer = dbObjectFactory.Create<Manufacturer>();
			var manufacturers = await manufacturer.SelectMany();
			return Ok(manufacturers);
		}
	}
}
