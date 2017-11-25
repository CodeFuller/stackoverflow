using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.WebApiApplication.Controllers
{
	public interface ISomeDependency
	{
		
	}

	public class SomeDependency : ISomeDependency
	{
		
	}

    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
	    public TestController(BlogContext context, ISomeDependency dependency)
	    {
		    
	    }
    }
}