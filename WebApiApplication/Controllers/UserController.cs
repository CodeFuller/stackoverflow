using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
	public class UserController : Controller
	{
		[HttpGet]
		public IActionResult Profile(string username)
		{
			return Ok();
		}
	}
}
