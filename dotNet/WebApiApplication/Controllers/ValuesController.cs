using System.Collections.Generic;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
	public class Item
	{
		
	}

	public class CreateItemRequest : AuthenticatedRequest
	{
		public Item Item { get; set; }
	}

	public abstract class AuthenticatedRequest
	{
		public string AuthToken { get; set; }
	}

	public class ValuesController : ApiController
	{
		// POST api/values
		public void Post(CreateItemRequest request)
		{
		}
	}
}
