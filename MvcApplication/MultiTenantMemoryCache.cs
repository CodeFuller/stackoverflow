using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;

namespace MvcApplication
{
    public class MultiTenantMemoryCache : IMemoryCache
	{
		//	Dictionary with separate instance of IMemoryCache for each domain
		private readonly ConcurrentDictionary<string, IMemoryCache> viewLookupCache = new ConcurrentDictionary<string, IMemoryCache>();

		public bool TryGetValue(object key, out object value)
		{
			return GetCurrentTenantCache().TryGetValue(key, out value);
		}

		public ICacheEntry CreateEntry(object key)
		{
			return GetCurrentTenantCache().CreateEntry(key);
		}

		public void Remove(object key)
		{
			GetCurrentTenantCache().Remove(key);
		}

		private IMemoryCache GetCurrentTenantCache()
		{
			var currentDomain = MultiTenantHelper.CurrentRequestDomain;
			return viewLookupCache.GetOrAdd(currentDomain, domain => new MemoryCache(new MemoryCacheOptions()));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (var cache in viewLookupCache)
				{
					cache.Value.Dispose();
				}
			}
		}
	}
}
