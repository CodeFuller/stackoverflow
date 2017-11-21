using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
	//[RoutePrefix("api")]
	public class ConfigController : ApiController
	{
		[HttpGet, Route("api/{id}/flows/config")]
		public string SayHello([FromUri] string id)
		{
			return "Hello " + id;
		}
	}
}
