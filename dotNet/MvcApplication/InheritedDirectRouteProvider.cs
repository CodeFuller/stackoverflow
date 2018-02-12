using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Routing;

namespace MvcApplication
{
	public class InheritedDirectRouteProvider : DefaultDirectRouteProvider
	{
		protected override IReadOnlyList<IDirectRouteFactory> GetControllerRouteFactories(ControllerDescriptor controllerDescriptor)
		{
			return controllerDescriptor.GetCustomAttributes(typeof(IDirectRouteFactory), true).Cast<IDirectRouteFactory>().ToArray();
		}
	}
}
