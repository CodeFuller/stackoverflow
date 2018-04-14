using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class Manufacturer3Controller : Controller
	{
		private readonly IHttpContextAccessor contextAccessor;

		public Manufacturer3Controller(IHttpContextAccessor contextAccessor)
		{
			this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var manufacturer = await new Manufacturer(contextAccessor).SelectMany();
			//	or
			//	var manufacturer = await new Manufacturer(contextAccessor.HttpContext.User).SelectMany();
			return Ok(manufacturer);
		}
	}
}
