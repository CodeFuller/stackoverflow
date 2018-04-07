using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// POST api/values
		[HttpPost]
		public void Post([FromBody] TestModel value)
		{
			//MyEnum v = (MyEnum)7;
		}
	}
}
