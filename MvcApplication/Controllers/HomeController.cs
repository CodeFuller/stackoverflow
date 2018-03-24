using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var data = FakeController.RenderViewToString("Home", "Index", ViewData);
			return View();
		}
	}
}
