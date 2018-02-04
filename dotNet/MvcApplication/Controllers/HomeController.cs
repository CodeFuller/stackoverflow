using System.Net;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		//public HttpResponseMessage Index()
		//{
		//	return new HttpResponseMessage(HttpStatusCode.Accepted)
		//	{
		//		Content = new StringContent("test")
		//	};
		//}

		public ActionResult Index()
		{
			return new ContentResultEx(HttpStatusCode.Accepted, "test");
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