using Microsoft.AspNetCore.Mvc;
using NetCore.WebApiApplication.Models;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		public void Update([FromBody]TestInput testInput)
		{
			return;
		}
	}
}
