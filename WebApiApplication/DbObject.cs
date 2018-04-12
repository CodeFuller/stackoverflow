using Microsoft.AspNetCore.Http;

namespace WebApiApplication
{
	public abstract class DbObject
	{
		protected DbObject(IHttpContextAccessor contextAccessor)
		{
			var context = contextAccessor.HttpContext;

			//	Create instance of AuthenticatedUser based on context.User or other request data
		}
	}
}
