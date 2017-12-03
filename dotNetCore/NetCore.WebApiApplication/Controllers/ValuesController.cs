using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.WebApiApplication.Controllers
{
	public class School
	{
		public string SomeField => "Hello";
	}

	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IEnumerable<string> GetSchool()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		[HttpPut]
		public IActionResult Put([FromBody] School school)
		{
			try
			{
				//var schoolExists = _schoolRepository.SchoolExists(school.Id);

				//if (!schoolExists) return NotFound();

				//if (!ModelState.IsValid) return BadRequest();

				//var schoolData = Mapper.Map<School, Data.School>(school);

				//var updatedClass = _schoolRepository.UpdateSchool(schoolData);

				//if (!updatedClass) return Json(GetHttpResponseMessage(HttpStatusCode.InternalServerError));

				var route = CreatedAtAction("GetSchool", school);

				return route;
			}
			catch (Exception e)
			{
				throw;
				//return LogException(e);
			}
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
