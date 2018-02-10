using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NetCore.WebApiApplication.Controllers
{
	[Route("api/v1/DerivedControllerB")]
	public class DerivedControllerB : BaseController<TimeOff2, HRContext2>
	{
	}

	public class HRContext2 : DbContext
	{
	}

	public class TimeOff2 : BaseOptionType
	{
	}
}
