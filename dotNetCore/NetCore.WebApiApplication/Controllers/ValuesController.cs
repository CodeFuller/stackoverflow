using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IActionResult Get()
		{
			var stuff = new Collection<string>();
			for (int i = 0; i < 100000; i++)
			{
				stuff.Add($"hello{i}");
			}
			return Ok(stuff);
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
