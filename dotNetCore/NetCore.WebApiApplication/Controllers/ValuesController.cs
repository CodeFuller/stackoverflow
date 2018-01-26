using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		[NonAction]
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var responseHeaders = (FrameResponseHeaders)HttpContext.Response.Headers;
			responseHeaders.MakeCaseInsensitive();
		}

		// GET api/values
		[HttpGet]
		public string Get()
		{
			var responseHeaders = (FrameResponseHeaders)HttpContext.Response.Headers;
			responseHeaders.AddCaseInsensitiveHeader("Set-Cookie", "Cookies1");
			responseHeaders.AddCaseInsensitiveHeader("SET-COOKIE", "Cookies2");

			return "Hello";
		}
	}
}
