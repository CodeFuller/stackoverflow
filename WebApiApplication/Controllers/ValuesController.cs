using System.Threading.Tasks;
using System.Web.Http;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
	public class ValuesController : ApiController
	{
		[HttpPost]
		[Route("")]
		public async Task<int> Post(MyModel myModel)
		{
			Validate(myModel);

			if (!ModelState.IsValid)
			{
				//Stop the process and return a message...
				return 1;
			}

			//Continue with the process.
			//Call the BL, etc.

			await Task.CompletedTask;

			return 0;
		}
	}
}
