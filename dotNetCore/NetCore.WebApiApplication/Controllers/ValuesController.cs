using Microsoft.AspNetCore.Mvc;
using NetCore.WebApiApplication.Models;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	[DefaultFromBody]
	public class ValuesController : Controller
	{
		[HttpGet]
		public void Get()
		{
			
		}

		// POST api/values
		[HttpPost]
		public void Post(SomeData value)
		{
			// ...
		}
	}
}
