using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
	public class ValuesController : Controller
	{
		[HttpGet]
		[Route("GetAllSomething")]
		public async Task<IActionResult> GetAllSomething([FromHeader(Name = "Accept")]string accept)
		{
			bool generateLinks = !string.IsNullOrWhiteSpace(accept) && accept.ToLower().EndsWith("hateoas");

			//	...

			return await Task.FromResult(Ok(new SomeModel { SomeProperty = "Test" }));
		}
	}
}
