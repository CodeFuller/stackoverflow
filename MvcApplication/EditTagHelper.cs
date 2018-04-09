using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MvcApplication
{
	[HtmlTargetElement("edit")]
	public class EditTagHelper : TagHelper
	{
		[HtmlAttributeName("asp-for")]
		public ModelExpression aspFor { get; set; }

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		protected IHtmlGenerator _generator { get; set; }

		public EditTagHelper(IHtmlGenerator generator)
		{
			_generator = generator;
		}

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			TagBuilder instance = new TagBuilder("div");
			var propName = aspFor.ModelExplorer.Model.ToString();

			var modelExProp = aspFor.ModelExplorer.Container.Properties.Single(x => x.Metadata.PropertyName.Equals(propName));
			var propValue = modelExProp.Model;
			var propEditFormatString = modelExProp.Metadata.EditFormatString;

			var label = _generator.GenerateLabel(ViewContext, aspFor.ModelExplorer,
				propName, propName, new { @class = "col-md-2 control-label", @type = "email" });

			var typeOfProperty1 = modelExProp.ModelType;
			var typeOfProperty2 = propValue?.GetType();

			if (typeOfProperty1 == typeof(Boolean))
			{
				bool isChecked = propValue.ToString().ToLower() == "true";
				instance = _generator.GenerateCheckBox(ViewContext, aspFor.ModelExplorer, propName, isChecked, new { @class = "form-control" });
			}
		}
	}
}
