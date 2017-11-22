using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinApplication.Controllers
{
	[RoutePrefix("api/test")]
	[Authorize]
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
