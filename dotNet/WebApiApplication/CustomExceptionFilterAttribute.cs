using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using WebApiApplication.Controllers;

namespace WebApiApplication
{
	public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			//	Add  your exception handling here

			//	Check context.Exception and decide whether you need to handle it
			if (context.Exception is SqlException)
			{
				var controllerType = context.ActionContext.ControllerContext.Controller.GetType();

				if (controllerType == typeof(UserController))
				{
					context.Response = new HttpResponseMessage(HttpStatusCode.Conflict)
					{
						Content = new StringContent("User already exists")
					};
				}
				else
				{
					// Cover other controller types here
				}
			}
		}
	}
}
