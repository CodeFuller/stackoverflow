using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NetCore.WebApiApplication.Controllers
{
	public abstract class BaseController<TEntity, TContext> : Controller where TEntity : BaseOptionType, new() where TContext : DbContext
	{
		[HttpGet]
		public virtual Task<IActionResult> Get(int id)
		{
			return Task.FromResult<IActionResult>(Ok());
		}

		[HttpPost]
		public virtual IActionResult Post(/* ... */)
		{
			return CreatedAtAction(nameof(Get), new
			{
				//	Put actual id here
				id = 123
			},
			null);
		}
	}

	public class BaseOptionType
	{
	}

	public class BaseGenericOptionTypesController<T, T1>
	{
	}

	public interface IGenericRepository<T, T1>
	{
	}
}
