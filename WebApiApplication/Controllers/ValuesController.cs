using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		public ValuesController(SomeDataObject dataObject)
		{
		}

		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
	}
}
