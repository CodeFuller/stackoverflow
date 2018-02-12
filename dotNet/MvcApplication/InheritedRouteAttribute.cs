using System;
using System.Linq;
using System.Web.Mvc.Routing;

namespace MvcApplication
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class InheritedRouteAttribute : Attribute, IDirectRouteFactory
	{
		public string Name { get; set; }
		public int Order { get; set; }
		public string Template { get; }

		public InheritedRouteAttribute(string template)
		{
			this.Template = template;
		}

		public RouteEntry CreateRoute(DirectRouteFactoryContext context)
		{
			var controllerDescriptor = context.Actions.First().ControllerDescriptor;
			var controllerName = controllerDescriptor.ControllerName;
			string template;
			if (string.Equals(controllerName, "Default", StringComparison.OrdinalIgnoreCase))
			{
				template = this.Template.Replace("{controller}/", string.Empty);
			}
			else
			{
				template = this.Template.Replace("{controller}", controllerName);
			}

			IDirectRouteBuilder builder = context.CreateBuilder(template);
			builder.Name = this.Name ?? controllerName + "_Route";
			builder.Order = this.Order;

			return builder.Build();
		}
	}
}
