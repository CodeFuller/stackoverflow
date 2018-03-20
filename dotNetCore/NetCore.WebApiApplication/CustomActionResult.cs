using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.WebApiApplication
{
	public class CustomActionResult : IActionResult
	{
		public Task ExecuteResultAsync(ActionContext context)
		{
			return Task.CompletedTask;
		}
	}
}
