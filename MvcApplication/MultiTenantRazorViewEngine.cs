using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MvcApplication
{
	public class MultiTenantRazorViewEngine : RazorViewEngine
	{
		public MultiTenantRazorViewEngine(IRazorPageFactoryProvider pageFactory, IRazorPageActivator pageActivator, HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, RazorProject razorProject, ILoggerFactory loggerFactory, DiagnosticSource diagnosticSource)
			: base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, razorProject, loggerFactory, diagnosticSource)
		{
			//	Dirty hack: setting RazorViewEngine.ViewLookupCache property that does not have a setter.
			var field = typeof(RazorViewEngine).GetField("<ViewLookupCache>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
			field.SetValue(this, new MultiTenantMemoryCache());

			//	Asserting that ViewLookupCache property was set to instance of MultiTenantMemoryCache
			if (ViewLookupCache .GetType() != typeof(MultiTenantMemoryCache))
			{
				throw new InvalidOperationException("Failed to set multi-tenant memory cache");
			}
		}
	}
}
