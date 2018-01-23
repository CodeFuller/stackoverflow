using System.Web.Http;

namespace WebApiApplication.Controllers
{
	public class Data
	{
		public int NumericField { get; set; }

		public string StringField { get; set; }
	}
	
	public class ValuesController : ApiController
	{
		[HttpPost]
		public void Post([FromUriOrBody] Data data)
		{
			//	...
		}
	}
}
