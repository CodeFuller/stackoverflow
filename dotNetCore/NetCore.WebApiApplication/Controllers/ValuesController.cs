using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCore.WebApiApplication.Controllers
{
	/// <summary>
	/// Error handler for webapi calls. Adds tracking to base controller.
	/// </summary>
	public class ApiExceptionAttribute : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			var items = context.HttpContext.Items;
			if (items.ContainsKey("Tracking"))
			{
				Tracking tracking = (Tracking)items["Tracking"];
				tracking.Exception(context.Exception, true);
			}

			base.OnException(context);
		}
	}

	public class ApiTrackingAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var tracking = new Tracking();
			//...add info to tracking

			context.HttpContext.Items.Add("Tracking", tracking);
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			var items = context.HttpContext.Items;
			if (items.ContainsKey("Tracking"))
			{
				Tracking tracking = (Tracking) items["Tracking"];
				tracking.Save();
			}
		}
	}

	public class Tracking
	{
		public void Save()
		{
			//throw new System.NotImplementedException();
		}

		public void Exception(Exception contextException, bool b)
		{
			//throw new NotImplementedException();
		}
	}

	[ApiTracking]
	[ApiException]
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			throw new InvalidOperationException();
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
