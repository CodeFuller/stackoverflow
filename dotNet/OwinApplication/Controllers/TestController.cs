using System.Web.Http;

namespace OwinApplication.Controllers
{
	[RoutePrefix("api/test")]
	public class TestController : ApiController
	{
		[Route]
		[HttpGet]
		public string Test()
		{
			return "Hello";
		}
	}
}
