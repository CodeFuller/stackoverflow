using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/v1/DerivedControllerA")]
	public class DerivedControllerA : BaseController<TimeOff, HRContext>
	{
	}

	public class TimeOff : BaseOptionType
	{
	}

	public class HRContext : DbContext
	{
	}
}
