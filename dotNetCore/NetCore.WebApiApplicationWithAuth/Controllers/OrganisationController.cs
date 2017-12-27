using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.WebApiApplicationWithAuth.Controllers
{
	[Authorize]
	[ApiVersion("1.0")]
	[Produces("application/json")]
	[Route("api/V{ver:apiVersion}/Organisation")]
	public class OrganisationController : Controller
	{
		[HttpGet]
		public async Task<IEnumerable<string>> Get()
		{
			return await Task.FromResult(new [] {"String1", "String2"});
		}
	}
}
