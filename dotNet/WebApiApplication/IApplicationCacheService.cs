using System;
using System.Threading.Tasks;

namespace WebApiApplication
{
	public interface IApplicationCacheService
	{
		T GetOrSet<T>(string cacheKey, Func<T> getDataCallback) where T : class;

		Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getDataCallback) where T : class;
	}
}
