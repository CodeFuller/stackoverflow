using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	[RoutePrefix("")]
	public class HomeController : Controller
	{
		[Route("~/")]
		[Route("{ShopName?}", Order = 2)]
		public ActionResult Index(string ShopName)
		{
			return View();
		}
	}
}
