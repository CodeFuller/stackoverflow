using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	[InheritedRoute("app/{controller}/{action=index}/{id?}")]
	public abstract class MyBaseController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
