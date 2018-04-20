using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace WebApiApplication.Controllers
{
	public class ValuesController : ApiController
	{
		[Route("{organizationIds}")]
		public Guid Get([ModelBinder(typeof(CommaSeparatedBinder))]
			IList<string> organizationIds)
		{
			return Guid.NewGuid();
		}
	}
}
