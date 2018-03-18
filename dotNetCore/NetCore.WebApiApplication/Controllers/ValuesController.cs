using Microsoft.AspNetCore.Mvc;
using NetCore.WebApiApplication.Models;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		[HttpGet]
		public Example Demo()
		{
			return new Example("test");
		}
	}
}
