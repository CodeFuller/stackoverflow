using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication
{
	public class ContentResultEx : ContentResult
	{
		private readonly HttpStatusCode statusCode;

		public ContentResultEx(HttpStatusCode statusCode, string message)
		{
			this.statusCode = statusCode;
			Content = message;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			base.ExecuteResult(context);
			HttpResponseBase response = context.HttpContext.Response;
			response.StatusCode = (int)statusCode;
		}
	}
}
