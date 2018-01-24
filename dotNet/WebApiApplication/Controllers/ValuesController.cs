using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
	public class ValuesController : ApiController
	{
[HttpPost]
[Route("BridgeSP/{SPId}")]
public HttpResponseMessage Post(long SPId)
{
	//	...
			try
			{
				return Request.CreateResponse(HttpStatusCode.OK, "SP Bridged Successful ");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
			}
		}
	}
}
