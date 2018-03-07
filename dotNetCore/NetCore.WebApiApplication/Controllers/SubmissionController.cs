using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class SubmissionController : Controller
	{
		private readonly IRecurringJobManager recurringJobManager;

		public SubmissionController(IRecurringJobManager recurringJobManager)
		{
			this.recurringJobManager = recurringJobManager;
		}

		public IActionResult Post()
		{
			recurringJobManager.AddOrUpdate(() => InitiateSubmission(), Cron.Minutely);

			return Ok("Periodic submission triggered");
		}

		public void InitiateSubmission()
		{
			// ...
		}
	}
}
