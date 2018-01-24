using System.Collections.Generic;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
	public class SomeData
	{
		public string StringField { get; set; }

		public int NumericField { get; set; }
	}

	public class ValuesController : ApiController
	{
		// GET api/values
		public SomeData Get()
		{
			return new SomeData
			{
				StringField = "Hello there",
				NumericField = 123,
			};
		}

		public void Post(SomeData data)
		{
		}
	}
}
