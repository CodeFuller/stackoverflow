using Microsoft.AspNetCore.Mvc;
using NetCore.WebApiApplication.Models;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : BaseController
	{
		// POST api/values
		[HttpPost]
		public void Post(SomeData value)
		{
			// ...
		}
	}
}
