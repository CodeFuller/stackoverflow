using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiApplication.Controllers
{
	public class LogEntry
	{
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string comment { get; set; }
		public bool modified { get; set; }
		public DateTime inTime { get; set; }
		public DateTime outTime { get; set; }
		public double totalHrs { get; set; }
		public int logID { get; set; }
	}

	public class ValuesController : ApiController
	{
		// GET api/values
		public void Put(int id, LogEntry item)
		{
			if (item != null)
			{
				//DO STUFF
			}
		}
	}
}
