using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace NetCore.MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly IStringLocalizer<HomeController> _stringLocalizer;

		public HomeController(IStringLocalizer<HomeController> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
		}

		public IActionResult Index()
		{
			string testValue = _stringLocalizer["Test"];
			return View();
		}
	}
}
