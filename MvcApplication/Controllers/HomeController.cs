using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		//[Route("test/0", Order = 1)]
		[Route("{someParam}/{test:testConstraint2}", Order = 10)]
		public ActionResult Test0()
		{
			return Content("Test0");
		}

		[Route("{someParam}/{test:testConstraint}", Order = 10)]
		public ActionResult Test1()
		{
			return Content("Test1");
		}
	}
}
