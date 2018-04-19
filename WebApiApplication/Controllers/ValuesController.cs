using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			// The problem: this gets executed *before* the global filter.
			// I actually want the FooActionFilter to prepare this value for me.
			var foo = context.RouteData.Values.GetValueOrDefault("foo").ToString();
		}

		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
