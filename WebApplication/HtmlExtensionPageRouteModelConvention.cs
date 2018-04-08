using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebApplication
{
    public class HtmlExtensionPageRouteModelConvention : IPageRouteModelConvention
	{
		public void Apply(PageRouteModel model)
		{
			var selectorCount = model.Selectors.Count;
			for (var i = 0; i < selectorCount; ++i)
			{
				var attributeRouteModel = model.Selectors[i].AttributeRouteModel;
				if (String.IsNullOrEmpty(attributeRouteModel.Template))
				{
					continue;
				}

				attributeRouteModel.SuppressLinkGeneration = true;
				model.Selectors.Add(new SelectorModel
				{
					AttributeRouteModel = new AttributeRouteModel
					{
						Template = $"{attributeRouteModel.Template}.html",
					}
				});
			}
		}
	}
}
