using Microsoft.AspNetCore.Mvc;

namespace ChildApplication.ChildAppRoot.Controllers
{
	public class HomeController : Controller
	{
		[NamespaceConstraint]
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}
	}
}
