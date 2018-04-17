using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MvcApplication.Controllers
{
	public class MyController : Controller
	{
		public IActionResult Getuser(Guid id)
		{
			var path = HttpContext.Request.Path;
			var id2 = Guid.Parse(path.Value.Split('/').Last());

			return View();
		}
	}
}
