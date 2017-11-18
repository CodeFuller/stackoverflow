using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace MvcApplication
{
    public class DisplayMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var propertyAttributes = context.Attributes;
            DisplayMetadata modelMetadata = context.DisplayMetadata;
            var propertyName = context.Key.Name;

            modelMetadata.DisplayName = () => DateTime.Now.ToString("T");
        }
    }
}
