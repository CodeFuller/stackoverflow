using System;
using System.Threading.Tasks;
using System.Web;

namespace WebApiApplication
{
	public class ApplicationCachingService : IApplicationCacheService
	{
		public T GetOrSet<T>(string cacheKey, Func<T> getDataCallback)
			where T : class
		{
			T data = HttpContext.Current.Application[cacheKey] as T;
			if (data == null)
			{
				data = getDataCallback();
				HttpContext.Current.Application[cacheKey] = data;
			}
			return data;
		}

		public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataCallback) where T : class
		{
			T data = HttpContext.Current.Application[cacheKey] as T;
			if (data == null)
			{
				data = await getDataCallback();
				HttpContext.Current.Application[cacheKey] = data;
			}
			return data;
		}

		public void ClearCache(string cacheKey)
		{
			HttpContext.Current.Application[cacheKey] = null;
		}
	}
}
