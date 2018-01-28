using System.Web.Http;
using System.Web.Http.Results;

namespace WebApiApplication.Controllers
{
	public class HomeController : ApiController
	{
		[HttpPost]
		public IHttpActionResult ProcessFields([FromUri] string field1, [FromUri] string field2, [FromBody] string field3, [FromUri] string field4)
		{
			return new OkResult(Request);
		}
	}
}
