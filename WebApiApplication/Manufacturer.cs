using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApiApplication
{
	public class Manufacturer : DbObject<Manufacturer>
	{
		public Manufacturer(IHttpContextAccessor contextAccessor) : base(contextAccessor)
		{
		}

		public Task<object> SelectMany()
		{
			throw new System.NotImplementedException();
		}
	}
}
