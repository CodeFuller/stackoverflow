using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
	public class ValuesController : Controller
	{
		[HttpGet]
		[Route("GetAllSomething/{format}")]
		[FormatFilter]
		public async Task<IActionResult> GetAllSomething(string format)
		{
			bool generateLinks = !string.IsNullOrWhiteSpace(format) && format.ToLower().EndsWith("hateoas");

			//	...

			return await Task.FromResult(Ok(new SomeModel { SomeProperty = "Test" }));
		}
	}
}
