using System;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
	public class SomeClass
	{
		public string T { get; set; }
	}

	public class GoldenController : ApiController
	{
		[HttpPost]
		public IHttpActionResult Payout([FromBody] string t)
		{
			throw new NotImplementedException();
		}
	}
}
