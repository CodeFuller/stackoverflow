using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	[RoutePrefix("app/support")]
	[Route("{action=Index}/{id?}")]
	public class SupportController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
