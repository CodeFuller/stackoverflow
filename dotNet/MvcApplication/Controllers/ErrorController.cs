using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class ErrorController : Controller
	{
		public ViewResult Index()
		{
			return View();
		}

		public ViewResult NotFound()
		{
			Response.StatusCode = 404;
			return View("NotFound");
		}
	}
}
