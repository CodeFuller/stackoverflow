using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RazorGenerator.Mvc;

namespace MvcApplication.Controllers
{
	class FakeController : ControllerBase
	{
		protected override void ExecuteCore()
		{
		}

		public static string RenderViewToString(string controllerName, string viewName, object viewData)
		{
			using (var writer = new StringWriter())
			{
				var routeData = new RouteData();
				routeData.Values.Add("controller", controllerName);
				var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, new FakeController());
				var viewEngine = ViewEngines.Engines.OfType<PrecompiledMvcEngine>().FirstOrDefault();
				if (viewEngine == null)
				{
					throw new InvalidOperationException("PrecompiledMvcEngine is not registered");
				}
				var viewResult = viewEngine.FindView(fakeControllerContext, viewName, "", false);
				var viewContext = new ViewContext(fakeControllerContext, viewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
				viewResult.View.Render(viewContext, writer);
				return writer.ToString();
			}
		}
	}
}
