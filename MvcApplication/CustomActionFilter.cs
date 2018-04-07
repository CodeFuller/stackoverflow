using System.Web.Mvc;

namespace MvcApplication
{
	public class CustomActionFilter : ActionFilterAttribute
	{
		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			ViewResult viewResult = filterContext.Result as ViewResult;
			RazorView view = viewResult?.View as RazorView;
			string viewPath = view?.ViewPath;

			// ...
		}
	}
}
