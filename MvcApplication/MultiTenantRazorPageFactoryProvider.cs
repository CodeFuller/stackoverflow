using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace MvcApplication
{
	public class MultiTenantRazorPageFactoryProvider : IRazorPageFactoryProvider
	{
		//	Dictionary with separate instance of IMemoryCache for each domain
		private readonly ConcurrentDictionary<string, IRazorPageFactoryProvider> providers = new ConcurrentDictionary<string, IRazorPageFactoryProvider>();

		public RazorPageFactoryResult CreateFactory(string relativePath)
		{
			var currentDomain = MultiTenantHelper.CurrentRequestDomain;
			var factoryProvider = providers.GetOrAdd(currentDomain, domain => MultiTenantHelper.ServiceProvider.GetRequiredService<DefaultRazorPageFactoryProvider>());
			return factoryProvider.CreateFactory(relativePath);
		}
	}
}
