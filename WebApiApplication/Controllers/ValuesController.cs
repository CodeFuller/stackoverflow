using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		[HttpPost]
		public IActionResult Post([ModelBinder(BinderType = typeof(MyTypeBinder))] MyType model)
		{
			return Ok();
		}
	}
}
