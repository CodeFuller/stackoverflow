using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	[RoutePrefix("Panel")]
	public class PanelController : Controller
	{
		[Route("", Order = 1)]
		public ActionResult Index()
		{
			return View();
		}
	}
}
