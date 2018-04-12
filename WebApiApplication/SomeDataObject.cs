using Microsoft.AspNetCore.Http;

namespace WebApiApplication
{
	public class SomeDataObject : DbObject
	{
		public SomeDataObject(IHttpContextAccessor contextAccessor) : base(contextAccessor)
		{
		}
	}
}
