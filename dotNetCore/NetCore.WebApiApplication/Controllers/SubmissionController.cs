using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/[controller]")]
	public class SubmissionController : Controller
	{
		private readonly IRecurringJobFacade recurringJobFacade;

		public SubmissionController(IRecurringJobFacade recurringJobFacade)
		{
			this.recurringJobFacade = recurringJobFacade;
		}

		public IActionResult Post()
		{
			recurringJobFacade.AddOrUpdate(() => InitiateSubmission(), Cron.Minutely);

			return Ok("Periodic submission triggered");
		}

		public void InitiateSubmission()
		{
			// ...
		}
	}
}
