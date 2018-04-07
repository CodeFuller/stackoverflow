using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		[HttpGet]
		public IActionResult Get([FromQuery] CustomModel data)
		{
			return Ok(data);
		}
	}
}
