using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
	public class BlogPostDataModel
	{
	}

	public interface IDataRepository
	{
		Task<IList<T>> GetAllAsync<T>() where T : class;
	}

	public class ValuesController : ApiController
	{
		private readonly IApplicationCacheService cachingService;
		private readonly IDataRepository repository;

		public ValuesController(IApplicationCacheService cachingService)
		{
			this.cachingService = cachingService;
		}

		// GET api/values
		public async Task  Get()
		{
			var items = await cachingService.GetOrSetAsync<IList<BlogPostDataModel>>(
				"BlogPostsIndex",
				() => repository.GetAllAsync<BlogPostDataModel>()
			);
		}
	}
}
