using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class Manufacturer1Controller : Controller
	{
		private readonly Manufacturer manufacturer;

		public Manufacturer1Controller(Manufacturer manufacturer)
		{
			this.manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var manufacturers = await manufacturer.SelectMany();
			return Ok(manufacturers);
		}

		//	...
	}
}
