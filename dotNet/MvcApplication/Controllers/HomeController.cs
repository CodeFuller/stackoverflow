using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			// used to test error pages :D
			int a = 5;
			int b = 0;
			int res = a / b;

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
