using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Options;

namespace MvcApplication
{
	public class CustomModelMetadataProvider : DefaultModelMetadataProvider
	{
		public CustomModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider)
			: base(detailsProvider)
		{
		}

		public CustomModelMetadataProvider(ICompositeMetadataDetailsProvider detailsProvider, IOptions<MvcOptions> optionsAccessor)
			: base(detailsProvider, optionsAccessor)
		{
		}

		public override ModelMetadata GetMetadataForType(Type modelType)
		{
			//  Optimization for intensively used System.Object
			if (modelType == typeof(object))
			{
				return base.GetMetadataForType(modelType);
			}

			var identity = ModelMetadataIdentity.ForType(modelType);
			DefaultMetadataDetails details = CreateTypeDetails(identity);

			//  This part contains the same logic as DefaultModelMetadata.DisplayMetadata property
			//  See https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/ModelBinding/Metadata/DefaultModelMetadata.cs

			var context = new DisplayMetadataProviderContext(identity, details.ModelAttributes);
			//  Here your implementation of IDisplayMetadataProvider will be called
			DetailsProvider.CreateDisplayMetadata(context);
			details.DisplayMetadata = context.DisplayMetadata;

			return CreateModelMetadata(details);
		}
	}
}
