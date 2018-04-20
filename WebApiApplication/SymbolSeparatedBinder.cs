using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebApiApplication
{
	public abstract class SymbolSeparatedBinder : IModelBinder
	{
		protected abstract char Separator { get; }

		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			//	Put all logic here. Use Separator property for splitting.
			//	...

			return true;
		}
	}
}
