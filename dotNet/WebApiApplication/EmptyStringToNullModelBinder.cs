using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebApiApplication
{
	public class EmptyStringToNullModelBinder : IModelBinder
	{
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			throw new NotImplementedException();
		}
	}
}
